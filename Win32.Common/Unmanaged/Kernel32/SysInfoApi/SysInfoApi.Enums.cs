namespace Win32.Common
{
    /// <inheritdoc/>
    internal partial class Unmanaged
    {
        /// <inheritdoc/>
        public partial class Kernel32
        {
            /// <summary>
            ///     SysInfoApi interactions.
            /// </summary>
            public partial class SysInfoApi
            {
                /// <summary>
                ///     The format of the computer name.
                /// </summary>
                public enum COMPUTER_NAME_FORMAT
                {
                    /// <summary>
                    ///     The NetBIOS name of the local computer. If the local computer is a node in a cluster, lpBuffer receives the NetBIOS name of the cluster virtual server.
                    /// </summary>
                    ComputerNameNetBIOS,
                    /// <summary>
                    ///     The DNS host name of the local computer. If the local computer is a node in a cluster, lpBuffer receives the DNS host name of the cluster virtual server.
                    /// </summary>
                    ComputerNameDnsHostname,
                    /// <summary>
                    ///     The name of the DNS domain assigned to the local computer. If the local computer is a node in a cluster, lpBuffer receives the DNS domain name of the cluster virtual server.
                    /// </summary>
                    ComputerNameDnsDomain,
                    /// <summary>
                    ///     The fully qualified DNS name that uniquely identifies the local computer. This name is a combination of the DNS host name and the DNS domain name, using the form HostName.DomainName. If the local computer is a node in a cluster, lpBuffer receives the fully qualified DNS name of the cluster virtual server.
                    /// </summary>
                    ComputerNameDnsFullyQualified,
                    /// <summary>
                    ///     The NetBIOS name of the local computer. If the local computer is a node in a cluster, lpBuffer receives the NetBIOS name of the local computer, not the name of the cluster virtual server.
                    /// </summary>
                    ComputerNamePhysicalNetBIOS,
                    /// <summary>
                    ///     The DNS host name of the local computer. If the local computer is a node in a cluster, lpBuffer receives the DNS host name of the local computer, not the name of the cluster virtual server.
                    /// </summary>
                    ComputerNamePhysicalDnsHostname,
                    /// <summary>
                    ///     The name of the DNS domain assigned to the local computer. If the local computer is a node in a cluster, lpBuffer receives the DNS domain name of the local computer, not the name of the cluster virtual server.
                    /// </summary>
                    ComputerNamePhysicalDnsDomain,
                    /// <summary>
                    ///     The fully qualified DNS name that uniquely identifies the computer. If the local computer is a node in a cluster, lpBuffer receives the fully qualified DNS name of the local computer, not the name of the cluster virtual server.
                    ///     The fully qualified DNS name is a combination of the DNS host name and the DNS domain name, using the form HostName.DomainName.
                    /// </summary>
                    ComputerNamePhysicalDnsFullyQualified
                }
                /// <summary>
                ///     The processor architecture of the installed operating system
                /// </summary>
                public enum PROCESSOR_ARCHITECTURE
                {
                    /// <summary>
                    ///     x64 (AMD or Intel)
                    /// </summary>
                    PROCESSOR_ARCHITECTURE_AMD64 = 9,
                    /// <summary>
                    ///     ARM
                    /// </summary>
                    PROCESSOR_ARCHITECTURE_ARM = 5,
                    /// <summary>
                    ///     ARM64
                    /// </summary>
                    PROCESSOR_ARCHITECTURE_ARM64 = 12,
                    /// <summary>
                    ///     Intel Itanium-based
                    /// </summary>
                    PROCESSOR_ARCHITECTURE_IA64 = 6,
                    /// <summary>
                    ///     x86
                    /// </summary>
                    PROCESSOR_ARCHITECTURE_INTEL = 0,
                    /// <summary>
                    ///     Unknown architecture.
                    /// </summary>
                    PROCESSOR_ARCHITECTURE_UNKNOWN = 0xffff
                }
                /// <summary>
                ///     The operating system product type.
                /// </summary>
                public enum PRODUCT_TYPE : uint
                {
                    /// <summary>
                    ///     Unknown Version.
                    /// </summary>
                    PRODUCT_UNDEFINED = 0x00000000,
                    /// <summary>
                    ///     Ultimate.
                    /// </summary>
                    PRODUCT_ULTIMATE = 0x00000001,
                    /// <summary>
                    ///     Home Basic.
                    /// </summary>
                    PRODUCT_HOME_BASIC = 0x00000002,
                    /// <summary>
                    ///     Home Premium.
                    /// </summary>
                    PRODUCT_HOME_PREMIUM = 0x00000003,
                    /// <summary>
                    ///     Enterprise.
                    /// </summary>
                    PRODUCT_ENTERPRISE = 0x00000004,
                    /// <summary>
                    ///     Home Basic N
                    /// </summary>
                    PRODUCT_HOME_BASIC_N = 0x00000005,
                    /// <summary>
                    ///     Business Version.
                    /// </summary>
                    PRODUCT_BUSINESS = 0x00000006,
                    /// <summary>
                    ///     Server Standard (full installation. For Server Core installations of Windows Server 2012 and later, use the method, Determining whether Server Core is running.)
                    /// </summary>
                    PRODUCT_STANDARD_SERVER = 0x00000007,
                    /// <summary>
                    ///     Server Datacenter (full installation. For Server Core installations of Windows Server 2012 and later, use the method, Determining whether Server Core is running.)
                    /// </summary>
                    PRODUCT_DATACENTER_SERVER = 0x00000008,
                    /// <summary>
                    ///     Windows Small Business Server
                    /// </summary>
                    PRODUCT_SMALLBUSINESS_SERVER = 0x00000009,
                    /// <summary>
                    ///     Server Enterprise (full installation)
                    /// </summary>
                    PRODUCT_ENTERPRISE_SERVER = 0x0000000A,
                    /// <summary>
                    ///     Starter
                    /// </summary>
                    PRODUCT_STARTER = 0x0000000B,
                    /// <summary>
                    ///     Server Datacenter (core installation, Windows Server 2008 R2 and earlier)
                    /// </summary>
                    PRODUCT_DATACENTER_SERVER_CORE = 0x0000000C,
                    /// <summary>
                    ///     Server Standard (core installation, Windows Server 2008 R2 and earlier)
                    /// </summary>
                    PRODUCT_STANDARD_SERVER_CORE = 0x0000000D,
                    /// <summary>
                    ///     Server Enterprise (core installation)
                    /// </summary>
                    PRODUCT_ENTERPRISE_SERVER_CORE = 0x0000000E,
                    /// <summary>
                    ///     Server Enterprise for Itanium-based Systems
                    /// </summary>
                    PRODUCT_ENTERPRISE_SERVER_IA64 = 0x0000000F,
                    /// <summary>
                    ///     Business N
                    /// </summary>
                    PRODUCT_BUSINESS_N = 0x00000010,
                    /// <summary>
                    ///     Web Server (full installation)
                    /// </summary>
                    PRODUCT_WEB_SERVER = 0x00000011,
                    /// <summary>
                    ///     HPC Edition
                    /// </summary>
                    PRODUCT_CLUSTER_SERVER = 0x00000012,
                    /// <summary>
                    ///     Windows Storage Server 2008 R2 Essentials
                    /// </summary>
                    PRODUCT_HOME_SERVER = 0x00000013,
                    /// <summary>
                    ///     Storage Server Express
                    /// </summary>
                    PRODUCT_STORAGE_EXPRESS_SERVER = 0x00000014,
                    /// <summary>
                    ///     Storage Server Standard
                    /// </summary>
                    PRODUCT_STORAGE_STANDARD_SERVER = 0x00000015,
                    /// <summary>
                    ///     
                    /// </summary>
                    PRODUCT_STORAGE_WORKGROUP_SERVER = 0x00000016,
                    /// <summary>
                    ///     Storage Server Workgroup
                    /// </summary>
                    PRODUCT_STORAGE_ENTERPRISE_SERVER = 0x00000017,
                    /// <summary>
                    ///     
                    /// </summary>
                    PRODUCT_SERVER_FOR_SMALLBUSINESS = 0x00000018,
                    /// <summary>
                    ///     Windows Server 2008 for Windows Essential Server Solutions
                    /// </summary>
                    PRODUCT_SMALLBUSINESS_SERVER_PREMIUM = 0x00000019,
                    /// <summary>
                    ///     
                    /// </summary>
                    PRODUCT_HOME_PREMIUM_N = 0x0000001A,
                    /// <summary>
                    ///     Home Premium N
                    /// </summary>
                    PRODUCT_ENTERPRISE_N = 0x0000001B,
                    /// <summary>
                    ///     
                    /// </summary>
                    PRODUCT_ULTIMATE_N = 0x0000001C,
                    /// <summary>
                    ///     Ultimate N
                    /// </summary>
                    PRODUCT_WEB_SERVER_CORE = 0x0000001D,
                    /// <summary>
                    ///     
                    /// </summary>
                    PRODUCT_MEDIUMBUSINESS_SERVER_MANAGEMENT = 0x0000001E,
                    /// <summary>
                    ///     Windows Essential Business Server Management Server
                    /// </summary>
                    PRODUCT_MEDIUMBUSINESS_SERVER_SECURITY = 0x0000001F,
                    /// <summary>
                    ///     Windows Essential Business Server Messaging Server
                    /// </summary>
                    PRODUCT_MEDIUMBUSINESS_SERVER_MESSAGING = 0x00000020,
                    /// <summary>
                    ///     Windows Server 2008 without Hyper-V for Windows Essential Server Solutions
                    /// </summary>
                    PRODUCT_SERVER_FOR_SMALLBUSINESS_V = 0x00000023,
                    /// <summary>
                    ///     Server Standard without Hyper-V
                    /// </summary>
                    PRODUCT_STANDARD_SERVER_V = 0x00000024,
                    /// <summary>
                    ///     Server Enterprise without Hyper-V (full installation)
                    /// </summary>
                    PRODUCT_ENTERPRISE_SERVER_V = 0x00000026,
                    /// <summary>
                    ///     Server Standard without Hyper-V (core installation)
                    /// </summary>
                    PRODUCT_STANDARD_SERVER_CORE_V = 0x00000028,
                    /// <summary>
                    ///     Server Enterprise without Hyper-V (core installation)
                    /// </summary>
                    PRODUCT_ENTERPRISE_SERVER_CORE_V = 0x00000029,
                    /// <summary>
                    ///     Microsoft Hyper-V Server
                    /// </summary>
                    PRODUCT_HYPERV = 0x0000002A,
                    /// <summary>
                    ///     Windows 10 Home
                    /// </summary>
                    PRODUCT_CORE = 0x00000065,
                    /// <summary>
                    ///     Windows 10 Home China
                    /// </summary>
                    PRODUCT_CORE_COUNTRYSPECIFIC = 0x00000063,
                    /// <summary>
                    ///     Windows 10 Home N
                    /// </summary>
                    PRODUCT_CORE_N = 0x00000062,
                    /// <summary>
                    ///     Windows 10 Home Single Language
                    /// </summary>
                    PRODUCT_CORE_SINGLELANGUAGE = 0x00000064,
                    /// <summary>
                    ///     Windows 10 Education
                    /// </summary>
                    PRODUCT_EDUCATION = 0x00000079,
                    /// <summary>
                    ///     Windows 10 Education N
                    /// </summary>
                    PRODUCT_EDUCATION_N = 0x0000007A,
                    /// <summary>
                    ///     Windows 10 Enterprise E
                    /// </summary>
                    PRODUCT_ENTERPRISE_E = 0x00000046,
                    /// <summary>
                    ///     Windows 10 Pro for Workstations
                    /// </summary>
                    PRODUCT_PRO_WORKSTATION = 0x000000A1,
                    /// <summary>
                    ///     Windows 10 Pro for Workstations N
                    /// </summary>
                    PRODUCT_PRO_WORKSTATION_N = 0x000000A2,
                    /// <summary>
                    ///     Windows 10 Pro
                    /// </summary>
                    PRODUCT_PROFESSIONAL = 0x00000030,
                    /// <summary>
                    ///     Windows 10 Pro N
                    /// </summary>
                    PRODUCT_PROFESSIONAL_N = 0x00000031
                }
                /// <summary>
                ///     Identifier of a firmware table provider for calls to EnumSystemFirmwareTables.
                /// </summary>
                public enum FirmwareTableProviderId : uint
                {
                    /// <summary>
                    ///     The ACPI firmware table provider.
                    /// </summary>
                    ACPI = (byte)'A' << 24 | (byte)'C' << 16 | (byte)'P' << 8 | (byte)'I',
                    /// <summary>
                    ///     The raw firmware table provider. Not supported for UEFI systems; use 'RSMB' instead.
                    /// </summary>
                    FIRM = (byte)'F' << 24 | (byte)'I' << 16 | (byte)'R' << 8 | (byte)'M',
                    /// <summary>
                    ///     The raw SMBIOS firmware table provider.
                    /// </summary>
                    RSMB = (byte)'R' << 24 | (byte)'S' << 16 | (byte)'M' << 8 | (byte)'B'
                }
            }
        }
    }
}
