// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Runtime.InteropServices;

#pragma warning disable CA1823 // unused private padding fields in MulticastOption structs

internal static partial class Interop
{
    internal static partial class Sys
    {
        internal enum MulticastOption : int
        {
            MULTICAST_ADD = 0,
            MULTICAST_DROP = 1,
            MULTICAST_IF = 2,

            MCAST_JOIN_GROUP = 7,
            MCAST_LEAVE_GROUP = 8,
            MCAST_BLOCK_SOURCE = 9,
            MCAST_UNBLOCK_SOURCE = 10,
            MCAST_JOIN_SOURCE_GROUP = 11,
            MCAST_LEAVE_SOURCE_GROUP = 12,
        }

        internal struct IPv4MulticastOption
        {
            public uint MulticastAddress;
            public uint LocalAddress;
            public int InterfaceIndex;
            private int _padding;
        }

        internal struct IPv6MulticastOption
        {
            public IPAddress Address;
            public int InterfaceIndex;
            private int _padding;
        }

        internal struct IPv4MulticastGroupOption
        {
            public uint Group;
            public uint InterfaceIndex;
        }

        internal struct IPv6MulticastGroupOption
        {
            public IPAddress Group;
            public uint InterfaceIndex;
            private int _padding;
        }

        internal struct IPv4SourceMulticastGroupOption
        {
            public uint Group;
            public uint Source;
            public uint InterfaceIndex;
            private int _padding;
        }

        internal struct IPv6SourceMulticastGroupOption
        {
            public IPAddress Group;
            public IPAddress Source;
            public uint InterfaceIndex;
            private int _padding;
        }

        [DllImport(Libraries.SystemNative, EntryPoint = "SystemNative_GetIPv4MulticastOption")]
        internal static extern unsafe Error GetIPv4MulticastOption(SafeHandle socket, MulticastOption multicastOption, IPv4MulticastOption* option);

        [DllImport(Libraries.SystemNative, EntryPoint = "SystemNative_SetIPv4MulticastOption")]
        internal static extern unsafe Error SetIPv4MulticastOption(SafeHandle socket, MulticastOption multicastOption, IPv4MulticastOption* option);

        [DllImport(Libraries.SystemNative, EntryPoint = "SystemNative_GetIPv6MulticastOption")]
        internal static extern unsafe Error GetIPv6MulticastOption(SafeHandle socket, MulticastOption multicastOption, IPv6MulticastOption* option);

        [DllImport(Libraries.SystemNative, EntryPoint = "SystemNative_SetIPv6MulticastOption")]
        internal static extern unsafe Error SetIPv6MulticastOption(SafeHandle socket, MulticastOption multicastOption, IPv6MulticastOption* option);

        [DllImport(Libraries.SystemNative, EntryPoint = "SystemNative_SetIPv4MulticastGroupOption")]
        internal static extern unsafe Error SetIPv4MulticastGroupOption(SafeHandle socket, MulticastOption multicastOption, IPv4MulticastGroupOption* option);

        [DllImport(Libraries.SystemNative, EntryPoint = "SystemNative_SetIPv6MulticastGroupOption")]
        internal static extern unsafe Error SetIPv6MulticastGroupOption(SafeHandle socket, MulticastOption multicastOption, IPv6MulticastGroupOption* option);

        [DllImport(Libraries.SystemNative, EntryPoint = "SystemNative_SetIPv4SourceMulticastGroupOption")]
        internal static extern unsafe Error SetIPv4SourceMulticastGroupOption(SafeHandle socket, MulticastOption multicastOption, IPv4SourceMulticastGroupOption* option);

        [DllImport(Libraries.SystemNative, EntryPoint = "SystemNative_SetIPv6SourceMulticastGroupOption")]
        internal static extern unsafe Error SetIPv6SourceMulticastGroupOption(SafeHandle socket, MulticastOption multicastOption, IPv6SourceMulticastGroupOption* option);
    }
}
