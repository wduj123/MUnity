using UnityEngine;
using System.Collections.Generic;

[global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"BuildingDB")]
public partial class BuildingDB : global::ProtoBuf.IExtensible
{
    public BuildingDB() {}
    
    private string _Id;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"Id", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string Id
    {
        get { return _Id; }
        set { _Id = value; }
    }
    private string _name;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"name", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string name
    {
        get { return _name; }
        set { _name = value; }
    }
    private string _des;
    [global::ProtoBuf.ProtoMember(3, IsRequired = true, Name=@"des", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string des
    {
        get { return _des; }
        set { _des = value; }
    }
    private int _uiType;
    [global::ProtoBuf.ProtoMember(4, IsRequired = true, Name=@"uiType", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int uiType
    {
        get { return _uiType; }
        set { _uiType = value; }
    }
    private int _type;
    [global::ProtoBuf.ProtoMember(5, IsRequired = true, Name=@"type", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int type
    {
        get { return _type; }
        set { _type = value; }
    }
    private string _resourceIcon;
    [global::ProtoBuf.ProtoMember(6, IsRequired = true, Name=@"resourceIcon", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string resourceIcon
    {
        get { return _resourceIcon; }
        set { _resourceIcon = value; }
    }
    private string _resId;
    [global::ProtoBuf.ProtoMember(7, IsRequired = true, Name=@"resId", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string resId
    {
        get { return _resId; }
        set { _resId = value; }
    }
    private int _palaceLv;
    [global::ProtoBuf.ProtoMember(8, IsRequired = true, Name=@"palaceLv", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int palaceLv
    {
        get { return _palaceLv; }
        set { _palaceLv = value; }
    }
    private int _useType;
    [global::ProtoBuf.ProtoMember(9, IsRequired = true, Name=@"useType", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int useType
    {
        get { return _useType; }
        set { _useType = value; }
    }
    private int _minLv;
    [global::ProtoBuf.ProtoMember(10, IsRequired = true, Name=@"minLv", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int minLv
    {
        get { return _minLv; }
        set { _minLv = value; }
    }
    private int _maxLv;
    [global::ProtoBuf.ProtoMember(11, IsRequired = true, Name=@"maxLv", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int maxLv
    {
        get { return _maxLv; }
        set { _maxLv = value; }
    }
    private string _dataId;
    [global::ProtoBuf.ProtoMember(12, IsRequired = true, Name=@"dataId", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string dataId
    {
        get { return _dataId; }
        set { _dataId = value; }
    }
    private int _sliver;
    [global::ProtoBuf.ProtoMember(13, IsRequired = true, Name=@"sliver", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int sliver
    {
        get { return _sliver; }
        set { _sliver = value; }
    }
    private int _wood;
    [global::ProtoBuf.ProtoMember(14, IsRequired = true, Name=@"wood", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int wood
    {
        get { return _wood; }
        set { _wood = value; }
    }
    private int _stone;
    [global::ProtoBuf.ProtoMember(15, IsRequired = true, Name=@"stone", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int stone
    {
        get { return _stone; }
        set { _stone = value; }
    }
    private int _food;
    [global::ProtoBuf.ProtoMember(16, IsRequired = true, Name=@"food", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int food
    {
        get { return _food; }
        set { _food = value; }
    }
    private int _lordExp;
    [global::ProtoBuf.ProtoMember(17, IsRequired = true, Name=@"lordExp", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int lordExp
    {
        get { return _lordExp; }
        set { _lordExp = value; }
    }
    private string _model;
    [global::ProtoBuf.ProtoMember(18, IsRequired = true, Name=@"model", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string model
    {
        get { return _model; }
        set { _model = value; }
    }
    private string _buildModel;
    [global::ProtoBuf.ProtoMember(19, IsRequired = true, Name=@"buildModel", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string buildModel
    {
        get { return _buildModel; }
        set { _buildModel = value; }
    }
    private Vector3 _position;
    [global::ProtoBuf.ProtoMember(20, IsRequired = true, Name=@"position", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public Vector3 position
    {
        get { return _position; }
        set { _position = value; }
    }
    private int _level;
    [global::ProtoBuf.ProtoMember(21, IsRequired = true, Name=@"level", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int level
    {
        get { return _level; }
        set { _level = value; }
    }
    private float _outPut;
    [global::ProtoBuf.ProtoMember(22, IsRequired = true, Name=@"outPut", DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
    public float outPut
    {
        get { return _outPut; }
        set { _outPut = value; }
    }
    private float _nextOutPut;
    [global::ProtoBuf.ProtoMember(23, IsRequired = true, Name=@"nextOutPut", DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
    public float nextOutPut
    {
        get { return _nextOutPut; }
        set { _nextOutPut = value; }
    }
    private int _outMax;
    [global::ProtoBuf.ProtoMember(24, IsRequired = true, Name=@"outMax", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int outMax
    {
        get { return _outMax; }
        set { _outMax = value; }
    }
    private readonly global::System.Collections.Generic.List<int> _nextOutMax = new global::System.Collections.Generic.List<int>();
    [global::ProtoBuf.ProtoMember(25, Name = @"nextOutMax", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<int> nextOutMax
    {
        get { return _nextOutMax; }
    }
    private string _moduleName;
    [global::ProtoBuf.ProtoMember(26, IsRequired = true, Name=@"moduleName", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string moduleName
    {
        get { return _moduleName; }
        set { _moduleName = value; }
    }
    private int _resValue;
    [global::ProtoBuf.ProtoMember(27, IsRequired = true, Name=@"resValue", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int resValue
    {
        get { return _resValue; }
        set { _resValue = value; }
    }
    private int _currentMaxLv;
    [global::ProtoBuf.ProtoMember(28, IsRequired = true, Name=@"currentMaxLv", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int currentMaxLv
    {
        get { return _currentMaxLv; }
        set { _currentMaxLv = value; }
    }

    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
    { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
}
