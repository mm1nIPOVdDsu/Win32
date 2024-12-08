using System;
using System.Runtime.InteropServices;

namespace Win32.Common
{
    /// <inheritdoc/>
    internal partial class Unmanaged
    {
        /// <summary>
        ///     Header is used by Security and Identity.
        /// </summary>
        public partial class WinTrust
        {
            /// <summary>
            ///     The WINTRUST_FILE_INFO structure is used when calling WinVerifyTrust to verify an individual file.
            /// </summary>
            public struct WINTRUST_FILE_INFO : IDisposable
            {
                /// <summary>
                ///     Initializes a new instance of the <see cref="WINTRUST_FILE_INFO"/> class.
                /// </summary>
                /// <param name="fileName">Full path to file.</param>
                /// <param name="subject">A <see cref="Guid"/> that represents the subject type.</param>
                public WINTRUST_FILE_INFO(string fileName, Guid subject)
                {
                    cbStruct = (uint)Marshal.SizeOf(typeof(WINTRUST_FILE_INFO));
                    pcwszFilePath = fileName;

                    if (subject != Guid.Empty)
                    {
                        pgKnownSubject = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(Guid)));
                        Marshal.StructureToPtr(subject, pgKnownSubject, true);
                    }
                    else { pgKnownSubject = IntPtr.Zero; }

                    hFile = IntPtr.Zero;
                }

                /// <summary>
                ///     Count of bytes in this structure.
                /// </summary>
                public uint cbStruct;
                /// <summary>
                ///     Full path and file name of the file to be verified. This parameter cannot be NULL.
                /// </summary>
                [MarshalAs(UnmanagedType.LPTStr)]
                public string pcwszFilePath;
                /// <summary>
                ///     [Optional] File handle to the open file to be verified. This handle must be to a file that has at least read permission. This member can be set to NULL.
                /// </summary>
                public IntPtr hFile;
                /// <summary>
                ///     [Optional] Pointer to a GUID structure that specifies the subject type. This member can be set to NULL.
                /// </summary>
                public IntPtr pgKnownSubject;

                #region IDisposable Members
                /// <summary>
                ///     Performs application-defined tasks associated with freeing, releasing, or resetting
                ///     unmanaged resources.
                /// </summary>
                public void Dispose()
                {
                    Dispose(true);
                    GC.SuppressFinalize(this);
                }
                /// <summary>
                ///     Performs application-defined tasks associated with freeing, releasing, or resetting
                ///     unmanaged resources.
                /// </summary>
                /// <param name="disposing">If true, Managed and unmanaged resources can be disposed.</param>
                private void Dispose(bool disposing)
                {
                    if (pgKnownSubject == IntPtr.Zero)
                    {
                        return;
                    }

                    if (disposing)
                    {
                        // do stuff here for managed I think
                    }

                    Marshal.DestroyStructure(pgKnownSubject, typeof(Guid));
                    Marshal.FreeHGlobal(pgKnownSubject);
                }
                #endregion
            }
            /// <summary>
            ///     The WINTRUST_DATA structure is used when calling WinVerifyTrust to pass necessary information into the trust providers.
            /// </summary>
            [StructLayout(LayoutKind.Sequential)]
            public struct WINTRUST_DATA : IDisposable
            {
                /// <summary>
                ///     Initializes a new instance of the <see cref="WINTRUST_DATA"/> class.
                /// </summary>
                /// <param name="fileInfo"><see cref="WINTRUST_FILE_INFO"/></param>
                public WINTRUST_DATA(WINTRUST_FILE_INFO fileInfo)
                {
                    cbStruct = (uint)Marshal.SizeOf(typeof(WINTRUST_DATA));
                    pInfoStruct = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(WINTRUST_FILE_INFO)));
                    Marshal.StructureToPtr(fileInfo, pInfoStruct, false);
                    dwUnionChoice = UnionChoice.File;

                    pPolicyCallbackData = IntPtr.Zero;
                    pSIPCallbackData = IntPtr.Zero;

                    dwUIChoice = UiChoice.NoUI;
                    fdwRevocationChecks = RevocationCheckFlags.None;
                    dwStateAction = StateAction.Ignore;
                    hWVTStateData = IntPtr.Zero;
                    pwszURLReference = IntPtr.Zero;
                    dwProvFlags = TrustProviderFlags.Safer;

                    dwUIContext = UIContext.Execute;
                }

                /// <summary>
                ///     The size, in bytes, of this structure.
                /// </summary>
                public uint cbStruct;
                /// <summary>
                ///     A pointer to a data buffer used to pass policy-specific data to a policy provider. This member can be NULL.
                /// </summary>
                public IntPtr pPolicyCallbackData;
                /// <summary>
                ///     A pointer to a data buffer used to pass subject interface package (SIP)-specific data to a SIP provider. This member can be NULL.
                /// </summary>
                public IntPtr pSIPCallbackData;
                /// <summary>
                ///     Specifies the kind of user interface (UI) to be used. 
                /// </summary>
                public UiChoice dwUIChoice;
                /// <summary>
                ///     Certificate revocation check options. This member can be set to add revocation checking to that done by the selected policy provider.
                /// </summary>
                public RevocationCheckFlags fdwRevocationChecks;
                /// <summary>
                ///     Specifies the union member to be used and, thus, the type of object for which trust will be verified.
                /// </summary>
                public UnionChoice dwUnionChoice;
                /// <summary>
                ///     A pointer to a WINTRUST_CERT_INFO structure.
                /// </summary>
                public IntPtr pInfoStruct;
                /// <summary>
                ///     Specifies the action to be taken. This can be one of the following values.
                /// </summary>
                public StateAction dwStateAction;
                /// <summary>
                ///     A handle to the state data. The contents of this member depends on the value of the dwStateAction member.
                /// </summary>
                public IntPtr hWVTStateData;
                /// <summary>
                ///     Reserved for future use. Set to NULL.
                /// </summary>
                private readonly IntPtr pwszURLReference;
                /// <summary>
                ///     DWORD value that specifies trust provider settings. This can be a bitwise combination of zero or more of the following values.
                /// </summary>
                public TrustProviderFlags dwProvFlags;
                /// <summary>
                ///     A DWORD value that specifies the user interface context for the WinVerifyTrust function. This causes the text in the Authenticode dialog box to match the action taken on the file.
                /// </summary>
                public UIContext dwUIContext;

                #region IDisposable Members
                /// <summary>
                ///     Performs application-defined tasks associated with freeing, releasing, or resetting
                ///     unmanaged resources.
                /// </summary>
                public void Dispose()
                {
                    Dispose(true);
                    GC.SuppressFinalize(this);
                }
                /// <summary>
                ///     Performs application-defined tasks associated with freeing, releasing, or resetting
                ///     unmanaged resources.
                /// </summary>
                /// <param name="disposing">If true, Managed and unmanaged resources can be disposed.</param>
                private void Dispose(bool disposing)
                {
                    if (dwUnionChoice == UnionChoice.File)
                    {

                        var info = new WINTRUST_FILE_INFO();
                        Marshal.PtrToStructure(pInfoStruct, info);
                        info.Dispose();
                        Marshal.DestroyStructure(pInfoStruct, typeof(WINTRUST_FILE_INFO));
                    }

                    if (disposing)
                    {
                        // do stuff for managed resources
                    }

                    Marshal.FreeHGlobal(pInfoStruct);
                }
                #endregion
            }
            /// <summary>
            ///     Wrapper for an unmanaged pointer.
            /// </summary>
            public sealed class UnmanagedPointer : IDisposable
            {
                private readonly AllocMethod m_meth;
                private IntPtr m_ptr;

                /// <summary>
                ///     Initializes a new instance of the <see cref="WINTRUST_DATA"/> class.
                /// </summary>
                /// <param name="ptr">The point as an <see cref="IntPtr"/>.</param>
                /// <param name="method"><see cref="AllocMethod"/></param>
                internal UnmanagedPointer(IntPtr ptr, AllocMethod method)
                {
                    m_meth = method;
                    m_ptr = ptr;
                }

                /// <summary>
                ///     Deconstructor.
                /// </summary>
                ~UnmanagedPointer()
                {
                    Dispose(false);
                }

                #region IDisposable Members
                /// <summary>
                ///     Performs application-defined tasks associated with freeing, releasing, or resetting
                ///     unmanaged resources.
                /// </summary>
                public void Dispose()
                {
                    Dispose(true);
                    GC.SuppressFinalize(this);
                }
                /// <summary>
                ///     Performs application-defined tasks associated with freeing, releasing, or resetting
                ///     unmanaged resources.
                /// </summary>
                /// <param name="disposing">If true, Managed and unmanaged resources can be disposed.</param>
                private void Dispose(bool disposing)
                {
                    if (m_ptr != IntPtr.Zero)
                    {
                        if (m_meth == AllocMethod.HGlobal)
                        {
                            Marshal.FreeHGlobal(m_ptr);
                        }
                        else if (m_meth == AllocMethod.CoTaskMem)
                        {
                            Marshal.FreeCoTaskMem(m_ptr);
                        }

                        m_ptr = IntPtr.Zero;
                    }

                    if (disposing)
                    {
                        // clean up managed code
                    }
                }
                #endregion

                /// <summary>
                ///     Explicit conversion from an <see cref="UnmanagedPointer"/> to an <see cref="IntPtr"/>.
                /// </summary>
                /// <param name="ptr"></param>
                public static implicit operator IntPtr(UnmanagedPointer ptr)
                {
                    return ptr.m_ptr;
                }
            }
        }
    }
}
