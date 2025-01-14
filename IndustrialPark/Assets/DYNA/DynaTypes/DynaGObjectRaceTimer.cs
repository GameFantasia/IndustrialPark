﻿using HipHopFile;
using System.ComponentModel;

namespace IndustrialPark
{
    public class DynaGObjectRaceTimer : AssetDYNA
    {
        private const string dynaCategoryName = "game_object:RaceTimer";

        protected override short constVersion => 2;

        [Category(dynaCategoryName)]
        public int CountDown { get; set; }
        [Category(dynaCategoryName)]
        public int StartTime { get; set; }
        [Category(dynaCategoryName)]
        public int VictoryTime { get; set; }
        [Category(dynaCategoryName)]
        public AssetSingle WarnTime1 { get; set; }
        [Category(dynaCategoryName)]
        public AssetSingle WarnTime2 { get; set; }
        [Category(dynaCategoryName)]
        public AssetSingle WarnTime3 { get; set; }

        public DynaGObjectRaceTimer(Section_AHDR AHDR, Game game, Endianness endianness) : base(AHDR, DynaType.game_object__RaceTimer, game, endianness)
        {
            using (var reader = new EndianBinaryReader(AHDR.data, endianness))
            {
                reader.BaseStream.Position = dynaDataStartPosition;

                CountDown = reader.ReadInt32();
                StartTime = reader.ReadInt32();
                VictoryTime = reader.ReadInt32();
                WarnTime1 = reader.ReadSingle();
                WarnTime2 = reader.ReadSingle();
                WarnTime3 = reader.ReadSingle();
            }
        }

        protected override byte[] SerializeDyna(Game game, Endianness endianness)
        {
            using (var writer = new EndianBinaryWriter(endianness))
            {
                writer.Write(CountDown);
                writer.Write(StartTime);
                writer.Write(VictoryTime);
                writer.Write(WarnTime1);
                writer.Write(WarnTime2);
                writer.Write(WarnTime3);

                return writer.ToArray();
            }
        }
    }
}