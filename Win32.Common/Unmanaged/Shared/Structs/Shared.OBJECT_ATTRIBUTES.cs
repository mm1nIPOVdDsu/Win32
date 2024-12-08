using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

using static Win32.Common.Unmanaged.AdvApi32.WinReg;

namespace Win32.Common
{
    /// <inheritdoc/>
    internal partial class Unmanaged
    {
        /// <summary>
        ///     Shared interactions.
        /// </summary>
        public partial class Shared
        {
            /// <summary>
            ///     The OBJECT_ATTRIBUTES structure specifies attributes that can be applied to objects or object handles by routines that create objects and/or return handles to objects.
            /// </summary>
            /// <see href="https://learn.microsoft.com/en-us/windows/win32/api/ntdef/ns-ntdef-_object_attributes">OBJECT_ATTRIBUTES</see>
            [StructLayoutAttribute(LayoutKind.Sequential)]
            public struct OBJECT_ATTRIBUTES 
            {
                /// <summary>
                ///     The number of bytes of data contained in this structure. The InitializeObjectAttributes macro sets this member to sizeof(OBJECT_ATTRIBUTES).
                /// </summary>
                public int Length;
                /// <summary>
                ///     Optional handle to the root object directory for the path name specified by the ObjectName member. If RootDirectory is NULL, ObjectName must point to a fully qualified object name that includes the full path to the target object. If RootDirectory is non-NULL, ObjectName specifies an object name relative to the RootDirectory directory. The RootDirectory handle can refer to a file system directory or an object directory in the object manager namespace.
                /// </summary>
                public IntPtr RootDirectory;
                /// <summary>
                ///     Pointer to a Unicode string that contains the name of the object for which a handle is to be opened. This must either be a fully qualified object name, or a relative path name to the directory specified by the RootDirectory member.
                /// </summary>
                public IntPtr ObjectName;
                /// <summary>
                ///     Bitmask of flags that specify <see cref="OBJ_ATTRIBUTES"/>.
                /// </summary>
                public OBJ_ATTRIBUTES Attributes;
                /// <summary>
                ///     Specifies a security descriptor (<see cref="SECURITY_DESCRIPTOR"/>) for the object when the object is created. If SecurityDescriptor is NULL, the object will receive default security settings. See DACL for a New Object.
                /// </summary>
                public IntPtr SecurityDescriptor;
                /// <summary>
                ///     
                /// </summary>
                public IntPtr SecurityQualityOfService;
            }
        }
    }
}



//using System;
//using System.Diagnostics;
//using System.Runtime.InteropServices;

//namespace Win32.Common
//{
//    /// <inheritdoc/>
//    internal partial class Unmanaged
//    {
//        /// <summary>
//        ///     Shared interactions.
//        /// </summary>
//        public partial class Shared
//        {

//        }
//    }
//}
