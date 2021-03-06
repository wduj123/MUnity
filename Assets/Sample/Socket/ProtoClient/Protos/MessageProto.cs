//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from: proto/Message.proto
namespace ProtoData
{
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"MessageHead")]
  public partial class MessageHead : global::ProtoBuf.IExtensible
  {
    public MessageHead() {}
    
    private int _version;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"version", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int version
    {
      get { return _version; }
      set { _version = value; }
    }
    private int _sequence;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"sequence", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int sequence
    {
      get { return _sequence; }
      set { _sequence = value; }
    }
    private int _stamp;
    [global::ProtoBuf.ProtoMember(3, IsRequired = true, Name=@"stamp", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int stamp
    {
      get { return _stamp; }
      set { _stamp = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"MessageData")]
  public partial class MessageData : global::ProtoBuf.IExtensible
  {
    public MessageData() {}
    
    private int _cmd;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"cmd", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int cmd
    {
      get { return _cmd; }
      set { _cmd = value; }
    }
    private ProtoData.MessageHead _head;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"head", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public ProtoData.MessageHead head
    {
      get { return _head; }
      set { _head = value; }
    }
    private byte[] _data;
    [global::ProtoBuf.ProtoMember(3, IsRequired = true, Name=@"data", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public byte[] data
    {
      get { return _data; }
      set { _data = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
    [global::ProtoBuf.ProtoContract(Name=@"MessageCode")]
    public enum MessageCode
    {
            
      [global::ProtoBuf.ProtoEnum(Name=@"SUCCESS", Value=0)]
      SUCCESS = 0,
            
      [global::ProtoBuf.ProtoEnum(Name=@"ERR_PROTOCOL", Value=1)]
      ERR_PROTOCOL = 1,
            
      [global::ProtoBuf.ProtoEnum(Name=@"ERR_SERVER", Value=2)]
      ERR_SERVER = 2,
            
      [global::ProtoBuf.ProtoEnum(Name=@"ERR_STORAGE", Value=3)]
      ERR_STORAGE = 3,
            
      [global::ProtoBuf.ProtoEnum(Name=@"ERR_INVALID_USER", Value=101)]
      ERR_INVALID_USER = 101,
            
      [global::ProtoBuf.ProtoEnum(Name=@"ERR_USER_STATE", Value=107)]
      ERR_USER_STATE = 107,
            
      [global::ProtoBuf.ProtoEnum(Name=@"ERR_SLOT_SERVER", Value=108)]
      ERR_SLOT_SERVER = 108
    }
  
}