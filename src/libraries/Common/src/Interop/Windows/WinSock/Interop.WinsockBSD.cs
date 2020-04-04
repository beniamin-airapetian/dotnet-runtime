// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Runtime.InteropServices;

internal static partial class Interop
{
    internal static partial class Winsock
    {
        // IO-Control operations are not directly exposed.
        // blocking is controlled by "Blocking" property on socket (FIONBIO)
        // amount of data available is queried by "Available" property (FIONREAD)
        // The other flags are not exposed currently.
        internal static class IoctlSocketConstants
        {
            public const int FIONREAD = 0x4004667F;
            public const int FIONBIO = unchecked((int)0x8004667E);
            public const int FIOASYNC = unchecked((int)0x8004667D);
            public const int SIOGETEXTENSIONFUNCTIONPOINTER = unchecked((int)0xC8000006);
            public const int AF_INET6 = 23;
            public const int AF_INET = 2;

            // Not likely to block (sync IO ok):
            //
            // FIONBIO
            // FIONREAD
            // SIOCATMARK
            // SIO_RCVALL
            // SIO_RCVALL_MCAST
            // SIO_RCVALL_IGMPMCAST
            // SIO_KEEPALIVE_VALS
            // SIO_ASSOCIATE_HANDLE (opcode setting: I, T==1)
            // SIO_ENABLE_CIRCULAR_QUEUEING (opcode setting: V, T==1)
            // SIO_GET_BROADCAST_ADDRESS (opcode setting: O, T==1)
            // SIO_GET_EXTENSION_FUNCTION_POINTER (opcode setting: O, I, T==1)
            // SIO_MULTIPOINT_LOOPBACK (opcode setting: I, T==1)
            // SIO_MULTICAST_SCOPE (opcode setting: I, T==1)
            // SIO_TRANSLATE_HANDLE (opcode setting: I, O, T==1)
            // SIO_ROUTING_INTERFACE_QUERY (opcode setting: I, O, T==1)
            //
            // Likely to block (recommended for async IO):
            //
            // SIO_FIND_ROUTE (opcode setting: O, T==1)
            // SIO_FLUSH (opcode setting: V, T==1)
            // SIO_GET_QOS (opcode setting: O, T==1)
            // SIO_GET_GROUP_QOS (opcode setting: O, I, T==1)
            // SIO_SET_QOS (opcode setting: I, T==1)
            // SIO_SET_GROUP_QOS (opcode setting: I, T==1)
            // SIO_ROUTING_INTERFACE_CHANGE (opcode setting: I, T==1)
            // SIO_ADDRESS_LIST_CHANGE (opcode setting: T==1)
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct TimeValue
        {
            public int Seconds;
            public int Microseconds;
        }

        // Argument structure for IP_ADD_MEMBERSHIP and IP_DROP_MEMBERSHIP.
        [StructLayout(LayoutKind.Sequential)]
        internal struct IPMulticastRequest
        {
            internal int MulticastAddress; // IP multicast address of group
            internal int InterfaceAddress; // local IP address of interface

            internal static readonly int Size = Marshal.SizeOf<IPMulticastRequest>();
        }

        // Argument structure for IP MCAST_JOIN_SOURCE_GROUP, MCAST_LEAVE_SOURCE_GROUP, MCAST_BLOCK_SOURCE and MCAST_UNBLOCK_SOURCE.
        [StructLayout(LayoutKind.Explicit)]
        internal struct IPMulticastGroupRequest
        {
            [FieldOffset(0)]
            internal uint InterfaceIndex; // Local interface index.

            [FieldOffset(8)]
            internal SocketIPAddressStorage MulticastAddress; // IP address of group.

            internal static readonly int Size = Marshal.SizeOf<IPSourceMulticastRequest>();
        }

        // Argument structure for IP MCAST_JOIN_SOURCE_GROUP, MCAST_LEAVE_SOURCE_GROUP, MCAST_BLOCK_SOURCE and MCAST_UNBLOCK_SOURCE.
        [StructLayout(LayoutKind.Explicit)]
        internal struct IPSourceMulticastRequest
        {
            [FieldOffset(0)]
            internal uint InterfaceIndex; // Local interface index.

            [FieldOffset(8)]
            internal SocketIPAddressStorage MulticastAddress; // IP address of group.

            [FieldOffset(136)]
            internal SocketIPAddressStorage SourceAddress; // IP address of source.

            internal static readonly int Size = Marshal.SizeOf<IPSourceMulticastRequest>();
        }

        [StructLayout(LayoutKind.Sequential, Size = 128)]
        internal struct SocketIPAddressStorage
        {
            internal ushort AddressFamily;
            internal ushort Port;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            internal byte[] IPAddress;

            public SocketIPAddressStorage(byte[] ipAddress)
            {
                AddressFamily = IoctlSocketConstants.AF_INET;
                Port = 0;
                IPAddress = ipAddress;
            }
        }

        // Argument structure for IPV6_ADD_MEMBERSHIP and IPV6_DROP_MEMBERSHIP.
        [StructLayout(LayoutKind.Sequential)]
        internal struct IPv6MulticastRequest
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            internal byte[] MulticastAddress; // IP address of group.
            internal int InterfaceIndex; // Local interface index.

            internal static readonly int Size = Marshal.SizeOf<IPv6MulticastRequest>();
        }

        // Argument structure for IP MCAST_JOIN_SOURCE_GROUP, MCAST_LEAVE_SOURCE_GROUP, MCAST_BLOCK_SOURCE and MCAST_UNBLOCK_SOURCE.
        [StructLayout(LayoutKind.Explicit)]
        internal struct IPv6MulticastGroupRequest
        {
            [FieldOffset(0)]
            internal uint InterfaceIndex; // Local interface index.

            [FieldOffset(8)]
            internal SocketIPv6AddressStorage MulticastAddress; // IP address of group.

            internal static readonly int Size = Marshal.SizeOf<IPSourceMulticastRequest>();
        }

        // Argument structure for IPv6 MCAST_JOIN_SOURCE_GROUP, MCAST_LEAVE_SOURCE_GROUP, MCAST_BLOCK_SOURCE and MCAST_UNBLOCK_SOURCE.
        [StructLayout(LayoutKind.Explicit)]
        internal struct IPv6SourceMulticastRequest
        {
            [FieldOffset(0)]
            internal uint InterfaceIndex; // Local interface index.

            [FieldOffset(8)]
            internal SocketIPv6AddressStorage MulticastAddress; // IP address of group.

            [FieldOffset(136)]
            internal SocketIPv6AddressStorage SourceAddress; // IP address of source.

            internal static readonly int Size = Marshal.SizeOf<IPv6SourceMulticastRequest>();
        }

        [StructLayout(LayoutKind.Sequential, Size = 128)]
        internal struct SocketIPv6AddressStorage
        {
            internal ushort AddressFamily;
            internal ushort Port;
            internal uint FlowInfo;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            internal byte[] IPv6Address;

            internal uint ScopeId;

            public SocketIPv6AddressStorage(byte[] ipv6Address)
            {
                AddressFamily = IoctlSocketConstants.AF_INET6;
                Port = 0;
                IPv6Address = ipv6Address;
                FlowInfo = 0;
                ScopeId = 0;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct Linger
        {
            internal ushort OnOff; // Option on/off.
            internal ushort Time; // Linger time in seconds.
        }
    }
}
