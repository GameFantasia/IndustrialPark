﻿using HipHopFile;
using SharpDX;
using System.ComponentModel;

namespace IndustrialPark
{
    public enum VolumeType
    {
        Unknown = 0,
        Sphere = 1,
        Box = 2,
        Cylinder_DoesntWork = 3,
    }

    public class AssetVOLU : BaseAsset, IRenderableAsset, IClickableAsset
    {
        private const string categoryName = "Volume";

        [Category(categoryName)]
        public FlagBitmask Flags { get; set; } = IntFlagsDescriptor();
        private VolumeType _shape;
        [Category(categoryName)]
        public VolumeType Shape
        {
            get => _shape;
            set
            {
                if (_shape != value)
                {
                    _shape = value;
                    switch (_shape)
                    {
                        case VolumeType.Sphere:
                            VolumeShape = new VolumeSphere();
                            break;
                        case VolumeType.Cylinder_DoesntWork:
                            VolumeShape = new VolumeCylinder();
                            break;
                        default:
                            VolumeShape = new VolumeBox();
                            break;
                    }
                }
            }
        }

        [Category(categoryName), TypeConverter(typeof(ExpandableObjectConverter))]
        public VolumeSpecific_Generic VolumeShape { get; set; }

        [Category(categoryName)]
        public int UnknownInt34 { get; set; }
        [Category(categoryName)]
        public int UnknownInt38 { get; set; }
        [Category(categoryName)]
        public int UnknownInt3C { get; set; }
        [Category(categoryName)]
        public int UnknownInt40 { get; set; }

        [Category(categoryName)]
        public AssetSingle Rotation { get; set; }
        [Category(categoryName)]
        public AssetSingle PivotX { get; set; }
        [Category(categoryName)]
        public AssetSingle PivotZ { get; set; }

        public AssetVOLU(Section_AHDR AHDR, Game game, Endianness endianness) : base(AHDR, game, endianness)
        {
            using (var reader = new EndianBinaryReader(AHDR.data, endianness))
            {
                reader.BaseStream.Position = baseHeaderEndPosition;

                Flags.FlagValueInt = reader.ReadUInt32();
                _shape = (VolumeType)reader.ReadByte();
                reader.ReadByte();
                reader.ReadByte();
                reader.ReadByte();
                switch (_shape)
                {
                    case VolumeType.Sphere:
                        VolumeShape = new VolumeSphere(reader);
                        break;
                    case VolumeType.Cylinder_DoesntWork:
                        VolumeShape = new VolumeCylinder(reader);
                        break;
                    default:
                        VolumeShape = new VolumeBox(reader);
                        break;
                }
                reader.BaseStream.Position = 0x34;
                UnknownInt34 = reader.ReadInt32();
                UnknownInt38 = reader.ReadInt32();
                UnknownInt3C = reader.ReadInt32();
                UnknownInt40 = reader.ReadInt32();
                Rotation = reader.ReadSingle();
                PivotX = reader.ReadSingle();
                PivotZ = reader.ReadSingle();
            }

            CreateTransformMatrix();
            ArchiveEditorFunctions.AddToRenderableAssets(this);
        }

        public override byte[] Serialize(Game game, Endianness endianness)
        {
            using (var writer = new EndianBinaryWriter(endianness))
            {
                writer.Write(SerializeBase(endianness));
                writer.Write(Flags.FlagValueInt);
                writer.Write((byte)_shape);
                writer.Write((byte)0);
                writer.Write((byte)0);
                writer.Write((byte)0);
                writer.Write(VolumeShape.Serialize(game, endianness));
                while (writer.BaseStream.Length < 0x34)
                    writer.Write((byte)0);
                writer.Write(UnknownInt34);
                writer.Write(UnknownInt38);
                writer.Write(UnknownInt3C);
                writer.Write(UnknownInt40);
                writer.Write(Rotation);
                writer.Write(PivotX);
                writer.Write(PivotZ);
                writer.Write(SerializeLinks(endianness));
                return writer.ToArray();
            }
        }

        [Browsable(false)]
        public AssetSingle PositionX { get => VolumeShape.CenterX; set => VolumeShape.CenterX = value; }
        [Browsable(false)]
        public AssetSingle PositionY { get => VolumeShape.CenterY; set => VolumeShape.CenterY = value; }
        [Browsable(false)]
        public AssetSingle PositionZ { get => VolumeShape.CenterZ; set => VolumeShape.CenterZ = value; }

        [Browsable(false)]
        public bool SpecialBlendMode => true;

        public static bool dontRender = false;

        public float? GetIntersectionPosition(SharpRenderer renderer, Ray ray)
        {
            if (!ShouldDraw(renderer))
                return null;

            return VolumeShape.GetIntersectionPosition(renderer, ray);
        }

        public BoundingBox GetBoundingBox() => VolumeShape.GetBoundingBox();
        public void CreateTransformMatrix() => VolumeShape.CreateTransformMatrix();
        public float GetDistanceFrom(Vector3 position) => VolumeShape.GetDistanceFrom(position);
        public bool ShouldDraw(SharpRenderer renderer) => VolumeShape.ShouldDraw(renderer, isSelected, dontRender, isInvisible);
        public void Draw(SharpRenderer renderer) => VolumeShape.Draw(renderer, isSelected);
    }
}