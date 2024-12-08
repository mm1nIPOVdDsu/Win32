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
            ///     Bitmask of flags that specify object handle attributes. NtDef.h
            /// </summary>
            public enum OBJ_ATTRIBUTES : uint
            {
                /// <summary>
                ///     This handle can be inherited by child processes of the current process.
                /// </summary>
                OBJ_INHERIT = 0x00000002,
                /// <summary>
                ///     This flag only applies to objects that are named within the object manager. By default, such objects are deleted when all open
                ///     handles to them are closed. If this flag is specified, the object is not deleted when all open handles are closed. Drivers can
                ///     use the ZwMakeTemporaryObject routine to make a permanent object non-permanent.
                /// </summary>
                OBJ_PERMANENT = 0x00000010,
                /// <summary>
                ///     If this flag is set and the OBJECT_ATTRIBUTES structure is passed to a routine that creates an object, the object can be
                ///     accessed exclusively. That is, once a process opens such a handle to the object, no other processes can open handles to this object.
                /// </summary>
                OBJ_EXCLUSIVE = 0x00000020,
                /// <summary>
                ///     If this flag is specified, a case-insensitive comparison is used when matching the name pointed to by the ObjectName member
                ///     against the names of existing objects. Otherwise, object names are compared using the default system settings.
                /// </summary>
                OBJ_CASE_INSENSITIVE = 0x00000040,
                /// <summary>
                ///     If this flag is specified, by using the object handle, to a routine that creates objects and if that object already exists,
                ///     the routine should open that object. Otherwise, the routine creating the object returns an NTSTATUS code of STATUS_OBJECT_NAME_COLLISION.
                /// </summary>
                OBJ_OPENIF = 0x00000080,
                /// <summary>
                ///     If an object handle, with this flag set, is passed to a routine that opens objects and if the object is a symbolic link
                ///     object, the routine should open the symbolic link object itself, rather than the object that the symbolic link refers to
                ///     (which is the default behavior).
                /// </summary>
                OBJ_OPENLINK = 0x00000100,
                /// <summary>
                ///     The handle is created in system process context and can only be accessed from kernel mode.
                /// </summary>
                OBJ_KERNEL_HANDLE = 0x00000200,
                /// <summary>
                ///     The routine that opens the handle should enforce all access checks for the object, even if the handle is being opened in
                ///     kernel mode.
                /// </summary>
                OBJ_FORCE_ACCESS_CHECK = 0x00000400,
                /// <summary>
                ///     If this flag is set, no reparse points will be followed when parsing the name of the associated object. If any reparses are
                ///     encountered the attempt will fail and return an STATUS_REPARSE_POINT_ENCOUNTERED result. This can be used to determine if
                ///     there are any reparse points in the object's path, in security scenarios.
                /// </summary>
                OBJ_DONT_REPARSE = 0x00000800,
                /// <summary>
                ///     A device map is a mapping between DOS device names and devices in the system, and is used when resolving DOS names. Separate
                ///     device maps exists for each user in the system, and users can manage their own device maps. Typically during impersonation,
                ///     the impersonated user's device map would be used. However, when this flag is set, the process user's device map is used instead.
                /// </summary>
                OBJ_IGNORE_IMPERSONATED_DEVICEMAP = 0x00001000,
                /// <summary>
                ///     Reserved.
                /// </summary>
                OBJ_VALID_ATTRIBUTES = 0x00001FF2,
            }
        }
    }
}
