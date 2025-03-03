﻿using HipHopFile;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace IndustrialPark
{
    public class AssetGenericBase : BaseAsset
    {
        private const string categoryName = "Generic Base Asset";

        [Category(categoryName)]
        public byte[] Data { get; set; }

        public AssetGenericBase(Section_AHDR AHDR, Game game, Endianness endianness) : base(AHDR, game, endianness)
        {
            Data = AHDR.data.Skip(baseHeaderEndPosition).Take(AHDR.data.Length - baseHeaderEndPosition - _links.Length * Link.sizeOfStruct).ToArray();
        }

        public override byte[] Serialize(Game game, Endianness endianness)
        {
            using (var writer = new EndianBinaryWriter(endianness))
            {
                writer.Write(SerializeBase(endianness));
                writer.Write(Data);
                writer.Write(SerializeLinks(endianness));
                return writer.ToArray();
            }
        }

        public override bool HasReference(uint assetID)
        {
            foreach (var u in Data_AsHex)
                if (u == assetID)
                    return true;

            return false;
        }

        [Category(categoryName)]
        public AssetID[] Data_AsHex
        {
            get
            {
                var values = new List<AssetID>();
                var reader = new EndianBinaryReader(Data, endianness);
                while (!reader.EndOfStream)
                    values.Add(reader.ReadUInt32());
                return values.ToArray();
            }
            set
            {
                using (var writer = new EndianBinaryWriter(endianness))
                {
                    foreach (var f in value)
                        writer.Write(f);
                    Data = writer.ToArray();
                }
            }
        }

        [Category(categoryName)]
        public AssetSingle[] Data_AsFloat
        {
            get
            {
                var values = new List<AssetSingle>();
                var reader = new EndianBinaryReader(Data, endianness);
                while (!reader.EndOfStream)
                    values.Add(reader.ReadSingle());
                return values.ToArray();
            }
            set
            {
                using (var writer = new EndianBinaryWriter(endianness))
                {
                    foreach (var f in value)
                        writer.Write(f);
                    Data = writer.ToArray();
                }
            }
        }
    }
}