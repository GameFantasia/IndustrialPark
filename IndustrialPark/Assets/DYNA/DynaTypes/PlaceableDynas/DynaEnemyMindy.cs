﻿using HipHopFile;
using System.Collections.Generic;
using System.ComponentModel;

namespace IndustrialPark
{
    public enum EnemyMindyType : uint
    {
        patrick_npc_bind = 0x7362A2AC,
        spongebob_npc_bind = 0xA92E3C8F,
        mindy_shell_bind = 0x5D13D2A4
    }

    public class DynaEnemyMindy : DynaEnemySB
    {
        private const string dynaCategoryName = "Enemy:SB:Mindy";

        protected override short constVersion => 3;

        [Category(dynaCategoryName)]
        public EnemyMindyType MindyType
        {
            get => (EnemyMindyType)(uint)Model_AssetID;
            set => Model_AssetID = (uint)value;
        }
        [Category(dynaCategoryName)]
        public AssetID TaskBox1_AssetID { get; set; }
        [Category(dynaCategoryName)]
        public AssetSingle ClamOpenDistance { get; set; }
        [Category(dynaCategoryName)]
        public AssetSingle ClamCloseDistance { get; set; }
        [Category(dynaCategoryName)]
        public AssetID TextBox_AssetID { get; set; }
        [Category(dynaCategoryName)]
        public int UnknownInt60 { get; set; }
        [Category(dynaCategoryName)]
        public AssetID TaskBox2_AssetID { get; set; }

        public DynaEnemyMindy(Section_AHDR AHDR, Game game, Endianness endianness) : base(AHDR, DynaType.Enemy__SB__Mindy, game, endianness)
        {
            using (var reader = new EndianBinaryReader(AHDR.data, endianness))
            {
                reader.BaseStream.Position = entityDynaEndPosition;

                TaskBox1_AssetID = reader.ReadUInt32();
                ClamOpenDistance = reader.ReadSingle();
                ClamCloseDistance = reader.ReadSingle();
                TextBox_AssetID = reader.ReadUInt32();
                UnknownInt60 = reader.ReadInt32();
                TaskBox2_AssetID = reader.ReadUInt32();
            }
        }

        protected override byte[] SerializeDyna(Game game, Endianness endianness)
        {
            using (var writer = new EndianBinaryWriter(endianness))
            {
                writer.Write(SerializeEntityDyna(endianness));
                writer.Write(TaskBox1_AssetID);
                writer.Write(ClamOpenDistance);
                writer.Write(ClamCloseDistance);
                writer.Write(TextBox_AssetID);
                writer.Write(UnknownInt60);
                writer.Write(TaskBox2_AssetID);

                return writer.ToArray();
            }
        }

        public override bool HasReference(uint assetID) =>
            TaskBox1_AssetID == assetID || TaskBox2_AssetID == assetID || TextBox_AssetID == assetID || base.HasReference(assetID);

        public override void Verify(ref List<string> result)
        {
            base.Verify(ref result);

            Verify(TaskBox1_AssetID, ref result);
            Verify(TaskBox2_AssetID, ref result);
            Verify(TextBox_AssetID, ref result);
        }
    }
}