﻿using HipHopFile;

namespace IndustrialPark
{
    public class AssetVIL : PlaceableAsset
    {
        public static bool dontRender = false;

        protected override bool DontRender()
        {
            return dontRender;
        }

        protected override int EventStartOffset
        {
            get => 0x6C + Offset;
        }

        public AssetVIL(Section_AHDR AHDR) : base(AHDR) { }

        public int Unknown1
        {
            get => ReadInt(0x54 + Offset);
            set => Write(0x54 + Offset, value);
        }

        public AssetID VilType
        {
            get => ReadUInt(0x58 + Offset);
            set => Write(0x58 + Offset, value);
        }

        public int Unknown2
        {
            get => ReadInt(0x5C + Offset);
            set => Write(0x5C + Offset, value);
        }

        public AssetID MVPTAssetID
        {
            get => ReadUInt(0x60 + Offset);
            set => Write(0x60 + Offset, value);
        }

        public AssetID DYNAAssetID_0
        {
            get => ReadUInt(0x64 + Offset);
            set => Write(0x64 + Offset, value);
        }

        public AssetID DYNAAssetID_1
        {
            get => ReadUInt(0x68 + Offset);
            set => Write(0x68 + Offset, value);
        }
    }
}