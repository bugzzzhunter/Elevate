// dllmain.cpp : Defines the entry point for the DLL application.
#include "pch.h"
#include <filesystem>   
#include <wtsapi32.h>
#include <Lmcons.h>
#include <iostream>
#include <string>
#include <Windows.h>
#include <wtsapi32.h>

#pragma comment(lib, "Wtsapi32.lib")

using namespace std;

wstring expandPath(const wchar_t* input) {
    wchar_t szEnvPath[MAX_PATH];
    ::ExpandEnvironmentStringsW(input, szEnvPath, MAX_PATH);
    return szEnvPath;
}

extern "C"             //No name mangling
__declspec(dllexport)  //Tells the compiler to export the function
void                    //Function return type     
__cdecl                //Specifies calling convention, cdelc is default, 

spawn(LPCWSTR module)
{
    
    FILE* pFile;
    const char* filename = "C:\\ProgramData\\ElevateApp\\logWatcher2.txt";
    //const char* filename = "C:\\ProgramData\\DemoApp\\logWatcher2.txt";
    pFile = fopen(filename, "a");

    if (pFile == NULL) {
        perror("Error opening file.");
    }
    else {
        fprintf(pFile, "\nSpawn Dll has been called!");
        fprintf(pFile, "\nModule called via dll is: %ws", module);
    }
    
    wchar_t exePath[MAX_PATH]{ 0x0000 };
    
    if (GetModuleFileName(NULL, exePath, MAX_PATH) == 0)
    {
        int ret = GetLastError();
        fprintf(pFile, "GetModuleFileName failed, error = %d\n", ret);
        // Return or however you want to handle an error.
    }
    else
    {
        std::wstring::size_type pos = std::wstring(exePath).find_last_of(L"\\/");
        auto exeDir = std::wstring(exePath).substr(0, pos);
        fprintf(pFile, "\nElevation service invoked dll from: %ws", exeDir.c_str());



        STARTUPINFO startInfo = { 0x00 };
        startInfo.cb = sizeof(startInfo);
        startInfo.wShowWindow = SW_SHOW;
        startInfo.lpDesktop = const_cast<wchar_t*>(L"WinSta0\\Default");

        PROCESS_INFORMATION procInfo = { 0x00 };

        HANDLE hToken = {};
        DWORD  sessionId = WTSGetActiveConsoleSessionId();

        OpenProcessToken(GetCurrentProcess(), TOKEN_ALL_ACCESS, &hToken);
        DuplicateTokenEx(hToken, TOKEN_ALL_ACCESS, nullptr, SecurityAnonymous, TokenPrimary, &hToken);

        SetTokenInformation(hToken, TokenSessionId, &sessionId, sizeof(sessionId));

        //std::wstring stemp = std::wstring(module.begin(), module.end());
        //LPCWSTR exe = stemp.c_str();
        std::wstring slash = L"\\";
        auto exe = exeDir.c_str()+slash+std::wstring(module);


        fprintf(pFile, "\nExe called via dll is: %ws", exe);

        if (CreateProcessAsUser(hToken,
            exe.c_str(),
            const_cast<wchar_t*>(L""),
            nullptr,
            nullptr,
            FALSE,
            NORMAL_PRIORITY_CLASS | CREATE_NEW_CONSOLE,
            nullptr,
            nullptr,
            &startInfo,
            &procInfo
        )
            ) {
            CloseHandle(procInfo.hProcess);
            CloseHandle(procInfo.hThread);
        }
    }

    fprintf(pFile, "\nRetruning dll value!");
    fclose(pFile);

    return;
}




/*BOOL APIENTRY DllMain(HMODULE hModule, DWORD  ul_reason_for_call, LPVOID lpReserved)
{

    //static const auto init = spawn(sys.argv[1]);

}*/

