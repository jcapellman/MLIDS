using System.Runtime.InteropServices;

namespace jcIDS.lib.Objects
{
    [StructLayout(LayoutKind.Explicit)]
    public struct IPHeader
    {
        [FieldOffset(0)] public byte ip_verlen;
        [FieldOffset(1)] public byte ip_tos;
        [FieldOffset(2)] public ushort ip_totallength;
        [FieldOffset(4)] public ushort ip_id;
        [FieldOffset(6)] public ushort ip_offset;
        [FieldOffset(8)] public byte ip_ttl;
        [FieldOffset(9)] public byte ip_protocol;
        [FieldOffset(10)] public ushort ip_checksum;
        [FieldOffset(12)] public uint ip_srcaddr;
        [FieldOffset(16)] public uint ip_destaddr;
    }
}