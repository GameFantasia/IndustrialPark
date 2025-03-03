﻿using HipHopFile;
using System.Collections.Generic;
using System.ComponentModel;

namespace IndustrialPark
{
    public class DynaEffectGrass : AssetDYNA
    {
        private const string dynaCategoryName = "effect:Grass";

        protected override short constVersion => 2;

        [Category(dynaCategoryName)]
        public AssetByte Unknown00 { get; set; }
        [Category(dynaCategoryName)]
        public AssetByte Unknown01 { get; set; }
        [Category(dynaCategoryName)]
        public AssetByte Unknown02 { get; set; }
        [Category(dynaCategoryName)]
        public AssetByte Unknown03 { get; set; }
        [Category(dynaCategoryName)]
        public AssetID GRSM_AssetID { get; set; }
        [Category(dynaCategoryName)]
        public AssetID MODL_AssetID { get; set; }
        [Category(dynaCategoryName)]
        public AssetSingle UnknownFloat01 { get; set; }
        [Category(dynaCategoryName)]
        public AssetSingle UnknownFloat02 { get; set; }
        [Category(dynaCategoryName)]
        public AssetSingle UnknownFloat03 { get; set; }
        [Category(dynaCategoryName)]
        public AssetSingle UnknownFloat04 { get; set; }
        [Category(dynaCategoryName)]
        public AssetSingle UnknownFloat05 { get; set; }
        [Category(dynaCategoryName)]
        public AssetSingle UnknownFloat06 { get; set; }

        public DynaEffectGrass(Section_AHDR AHDR, Game game, Endianness endianness) : base(AHDR, DynaType.effect__grass, game, endianness)
        {
            using (var reader = new EndianBinaryReader(AHDR.data, endianness))
            {
                reader.BaseStream.Position = dynaDataStartPosition;

                Unknown00 = reader.ReadByte();
                Unknown01 = reader.ReadByte();
                Unknown02 = reader.ReadByte();
                Unknown03 = reader.ReadByte();
                GRSM_AssetID = reader.ReadUInt32();
                MODL_AssetID = reader.ReadUInt32();
                UnknownFloat01 = reader.ReadSingle();
                UnknownFloat02 = reader.ReadSingle();
                UnknownFloat03 = reader.ReadSingle();
                UnknownFloat04 = reader.ReadSingle();
                UnknownFloat05 = reader.ReadSingle();
                UnknownFloat06 = reader.ReadSingle();
            }
        }

        protected override byte[] SerializeDyna(Game game, Endianness endianness)
        {
            using (var writer = new EndianBinaryWriter(endianness))
            {
                writer.Write(Unknown00);
                writer.Write(Unknown01);
                writer.Write(Unknown02);
                writer.Write(Unknown03);
                writer.Write(GRSM_AssetID);
                writer.Write(MODL_AssetID);
                writer.Write(UnknownFloat01);
                writer.Write(UnknownFloat02);
                writer.Write(UnknownFloat03);
                writer.Write(UnknownFloat04);
                writer.Write(UnknownFloat05);
                writer.Write(UnknownFloat06);

                return writer.ToArray();
            }
        }

        public override bool HasReference(uint assetID) => GRSM_AssetID == assetID || MODL_AssetID == assetID || base.HasReference(assetID);

        public override void Verify(ref List<string> result)
        {
            Verify(GRSM_AssetID, ref result);
            Verify(MODL_AssetID, ref result);

            base.Verify(ref result);
        }
    }
}