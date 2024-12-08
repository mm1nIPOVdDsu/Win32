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
            ///     The SECURITY_DESCRIPTOR_CONTROL type is a set of bit flags that qualify the meaning of a SECURITY_DESCRIPTOR structure or its
            ///     components. Each security descriptor has a Control member that stores the SECURITY_DESCRIPTOR_CONTROL bits.
            /// </summary>
            /// <see href="https://learn.microsoft.com/en-us/windows-hardware/drivers/ifs/security-descriptor-control">SECURITY_DESCRIPTOR_CONTROL</see>
            public enum SECURITY_DESCRIPTOR_CONTROL : ushort 
            {
                /// <summary>
                ///     A default mechanism, rather than the original provider of the security descriptor, provided the security descriptor's owner
                ///     security identifier (SID). To set this flag, use RtlSetOwnerSecurityDescriptor.
                /// </summary>
                SE_OWNER_DEFAULTED = 0x0001,
                /// <summary>
                ///     A default mechanism, rather than the original provider of the security descriptor, provided the security descriptor's group SID.
                /// </summary>
                SE_GROUP_DEFAULTED = 0x0002,
                /// <summary>
                ///     Indicates a security descriptor that has a DACL. If this flag isn't set, or if this flag is set and the DACL is NULL, the
                ///     security descriptor allows full access to everyone. This flag is used to hold the security information specified by a caller
                ///     until the security descriptor is associated with a securable object. Once the security descriptor is associated with a
                ///     securable object, the SE_DACL_PRESENT flag is always set in the security descriptor control. To set this flag, use RtlSetDaclSecurityDescriptor.
                /// </summary>
                SE_DACL_PRESENT = 0x0004,
                /// <summary>
                ///     Indicates a security descriptor with a default DACL. For example, if an object's creator doesn't specify a DACL, the object
                ///     receives the default DACL from the creator's access token. This flag can affect how the system treats the DACL, with respect
                ///     to ACE inheritance. The system ignores this flag if the SE_DACL_PRESENT flag isn't set. This flag is used to determine how the
                ///     final DACL on the object is to be computed and isn't stored physically in the security descriptor control of the securable
                ///     object. To set this flag, use RtlSetDaclSecurityDescriptor.
                /// </summary>
                SE_DACL_DEFAULTED = 0x0008,
                /// <summary>
                ///     Indicates a security descriptor that has a SACL.
                /// </summary>
                SE_SACL_PRESENT = 0x0010,
                /// <summary>
                ///     A default mechanism, rather than the original provider of the security descriptor, provided the SACL. This flag can affect how
                ///     the system treats the SACL, with respect to ACE inheritance. The system ignores this flag if the SE_SACL_PRESENT flag isn't set.
                /// </summary>
                SE_SACL_DEFAULTED = 0x0020,
                /// <summary>
                ///     A default mechanism, rather than the original provider of the security descriptor, provided the SACL. This flag can affect how
                ///     the system treats the SACL, with respect to ACE inheritance. The system ignores this flag if the SE_SACL_PRESENT flag isn't set.
                /// </summary>
                SE_DACL_UNTRUSTED = 0x0040,
                /// <summary>
                ///     Requests that the provider for the object protected by the security descriptor whose ACL should a server ACL based on the
                ///     input ACL, regardless of its source (explicit or defaulting). This is done by replacing all of the GRANT ACEs with compound
                ///     ACEs granting the current server. This flag is only meaningful if the subject is impersonating.
                /// </summary>
                SE_SERVER_SECURITY = 0x0080,
                /// <summary>
                ///     Requests that the provider for the object protected by the security descriptor automatically propagate the DACL to existing
                ///     child objects. If the provider supports automatic inheritance, it propagates the DACL to any existing child objects, and sets
                ///     the SE_DACL_AUTO_INHERITED bit in the security descriptors of the object and its child objects.
                /// </summary>
                SE_DACL_AUTO_INHERIT_REQ = 0x0100,
                /// <summary>
                ///     Requests that the provider for the object protected by the security descriptor automatically propagate the SACL to existing
                ///     child objects. If the provider supports automatic inheritance, it propagates the SACL to any existing child objects, and sets
                ///     the SE_SACL_AUTO_INHERITED bit in the security descriptors of the object and its child objects.
                /// </summary>
                SE_SACL_AUTO_INHERIT_REQ = 0x0200,
                /// <summary>
                ///     Starting with Windows 2000, indicates a security descriptor in which the DACL supports automatic propagation of inheritable
                ///     ACEs to existing child objects. For Windows 2000 ACLs that support autoinheritance, this bit is always set. It's used to
                ///     distinguish these ACLs from Windows NT 4.0 ACLs that don't support autoinheritance. This bit isn't set in security descriptors
                ///     for Windows NT 4.0 and earlier, which don't support automatic propagation of inheritable ACEs.
                /// </summary>
                SE_DACL_AUTO_INHERITED = 0x0400,
                /// <summary>
                ///     Indicates a security descriptor in which the SACL supports automatic propagation of inheritable ACEs to existing child
                ///     objects. This bit is set only if the automatic inheritance algorithm has been performed for the object and its existing child
                ///     objects. This bit isn't set in security descriptors for Windows NT 4.0 and earlier, which didn't support automatic propagation
                ///     of inheritable ACEs.
                /// </summary>
                SE_SACL_AUTO_INHERITED = 0x0800,
                /// <summary>
                ///     Protects the DACL of the security descriptor from being modified by inheritable ACEs.
                /// </summary>
                SE_DACL_PROTECTED = 0x1000,
                /// <summary>
                ///     Protects the SACL of the security descriptor from being modified by inheritable ACEs.
                /// </summary>
                SE_SACL_PROTECTED = 0x2000,
                /// <summary>
                ///     Indicates that the resource control manager bits in the security descriptor are valid. The Resource Manager control bits are
                ///     eight bits in the Sbz1 member of the SECURITY_DESCRIPTOR structure that contains information specific to the Resource Manager
                ///     accessing the structure. For more information, see the Windows SDK.
                /// </summary>
                SE_RM_CONTROL_VALID = 0x4000,
                /// <summary>
                ///     Indicates a security descriptor in self-relative format with all the security information in a contiguous block of memory. If
                ///     this flag isn't set, the security descriptor is in absolute format. For more information, see the Windows SDK documentation.
                /// </summary>
                SE_SELF_RELATIVE = 0x8000
            }
        }
    }
}
