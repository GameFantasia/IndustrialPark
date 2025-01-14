﻿using HipHopFile;
using System.Collections.Generic;
using System.ComponentModel;

namespace IndustrialPark
{
    public enum EnemyNeptuneType : uint
    {
        neptune_bind = 0x8F85AF53
    }

    public class DynaEnemyNeptune : DynaEnemySB
    {
        private const string dynaCategoryName = "Enemy:SB:Neptune";

        protected override short constVersion => 4;

        [Category(dynaCategoryName)]
        public EnemyNeptuneType NeptuneType
        {
            get => (EnemyNeptuneType)(uint)Model_AssetID;
            set => Model_AssetID = (uint)value;
        }
        [Category(dynaCategoryName)]
        public AssetID Unknown50 { get; set; }
        [Category(dynaCategoryName)]
        public AssetID Unknown54 { get; set; }

        public DynaEnemyNeptune(Section_AHDR AHDR, Game game, Endianness endianness) : base(AHDR, DynaType.Enemy__SB__Neptune, game, endianness)
        {
            using (var reader = new EndianBinaryReader(AHDR.data, endianness))
            {
                reader.BaseStream.Position = entityDynaEndPosition;

                Unknown50 = reader.ReadUInt32();
                Unknown54 = reader.ReadUInt32();
            }
        }

        protected override byte[] SerializeDyna(Game game, Endianness endianness)
        {
            using (var writer = new EndianBinaryWriter(endianness))
            {
                writer.Write(SerializeEntityDyna(endianness));
                writer.Write(Unknown50);
                writer.Write(Unknown54);

                return writer.ToArray();
            }
        }

        public override bool HasReference(uint assetID)
        {
            if (Unknown50 == assetID)
                return true;
            if (Unknown54 == assetID)
                return true;

            return base.HasReference(assetID);
        }

        public override void Verify(ref List<string> result)
        {
            base.Verify(ref result);

            Verify(Unknown50, ref result);
            Verify(Unknown54, ref result);
        }
    }
}