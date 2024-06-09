using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using EXP = System.ComponentModel.ExpandableObjectConverter;
using TC = System.ComponentModel.TypeConverterAttribute;


/*
    Copyright(c) 2017 Neodymium

    Permission is hereby granted, free of charge, to any person obtaining a copy
    of this software and associated documentation files (the "Software"), to deal
    in the Software without restriction, including without limitation the rights
    to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
    copies of the Software, and to permit persons to whom the Software is
    furnished to do so, subject to the following conditions:

    The above copyright notice and this permission notice shall be included in
    all copies or substantial portions of the Software.

    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
    IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
    FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
    AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
    LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
    OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
    THE SOFTWARE.



    //proudly mangled by dex

*/



namespace CodeWalker.GameFiles
{

    [TC(typeof(EXP))] public class ParticleEffectsList : ResourceFileBase
    {
        // pgBase
        // ptxFxList
        public override long BlockLength => 0x60;

        // structure data
        public ulong NamePointer { get; set; }
        public ulong Unknown_18h; // 0x0000000000000000
        public ulong TextureDictionaryPointer { get; set; }
        public ulong Unknown_28h; // 0x0000000000000000
        public ulong DrawableDictionaryPointer { get; set; }
        public ulong ParticleRuleDictionaryPointer { get; set; }
        public ulong Unknown_40h; // 0x0000000000000000
        public ulong EmitterRuleDictionaryPointer { get; set; }
        public ulong EffectRuleDictionaryPointer { get; set; }
        public ulong Unknown_58h; // 0x0000000000000000

        // reference data
        public string_r Name { get; set; }
        public TextureDictionary TextureDictionary { get; set; }
        public DrawablePtfxDictionary DrawableDictionary { get; set; }
        public ParticleRuleDictionary ParticleRuleDictionary { get; set; }
        public ParticleEffectRuleDictionary EffectRuleDictionary { get; set; }
        public ParticleEmitterRuleDictionary EmitterRuleDictionary { get; set; }


        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            base.Read(reader, parameters);

            // read structure data
            NamePointer = reader.ReadUInt64();
            Unknown_18h = reader.ReadUInt64();
            TextureDictionaryPointer = reader.ReadUInt64();
            Unknown_28h = reader.ReadUInt64();
            DrawableDictionaryPointer = reader.ReadUInt64();
            ParticleRuleDictionaryPointer = reader.ReadUInt64();
            Unknown_40h = reader.ReadUInt64();
            EmitterRuleDictionaryPointer = reader.ReadUInt64();
            EffectRuleDictionaryPointer = reader.ReadUInt64();
            Unknown_58h = reader.ReadUInt64();

            // read reference data
            Name = reader.ReadBlockAt<string_r>(NamePointer);
            TextureDictionary = reader.ReadBlockAt<TextureDictionary>(TextureDictionaryPointer);
            DrawableDictionary = reader.ReadBlockAt<DrawablePtfxDictionary>(DrawableDictionaryPointer);
            ParticleRuleDictionary = reader.ReadBlockAt<ParticleRuleDictionary>(ParticleRuleDictionaryPointer);
            EffectRuleDictionary = reader.ReadBlockAt<ParticleEffectRuleDictionary>(EmitterRuleDictionaryPointer);
            EmitterRuleDictionary = reader.ReadBlockAt<ParticleEmitterRuleDictionary>(EffectRuleDictionaryPointer);



            //if (Unknown_18h != 0)
            //{ }//no hit
            //if (Unknown_28h != 0)
            //{ }//no hit
            //if (Unknown_40h != 0)
            //{ }//no hit
            //if (Unknown_58h != 0)
            //{ }//no hit

        }
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            base.Write(writer, parameters);

            // update structure data
            NamePointer = (ulong)(Name != null ? Name.FilePosition : 0);
            TextureDictionaryPointer = (ulong)(TextureDictionary != null ? TextureDictionary.FilePosition : 0);
            DrawableDictionaryPointer = (ulong)(DrawableDictionary != null ? DrawableDictionary.FilePosition : 0);
            ParticleRuleDictionaryPointer = (ulong)(ParticleRuleDictionary != null ? ParticleRuleDictionary.FilePosition : 0);
            EmitterRuleDictionaryPointer = (ulong)(EffectRuleDictionary != null ? EffectRuleDictionary.FilePosition : 0);
            EffectRuleDictionaryPointer = (ulong)(EmitterRuleDictionary != null ? EmitterRuleDictionary.FilePosition : 0);

            // write structure data
            writer.Write(NamePointer);
            writer.Write(Unknown_18h);
            writer.Write(TextureDictionaryPointer);
            writer.Write(Unknown_28h);
            writer.Write(DrawableDictionaryPointer);
            writer.Write(ParticleRuleDictionaryPointer);
            writer.Write(Unknown_40h);
            writer.Write(EmitterRuleDictionaryPointer);
            writer.Write(EffectRuleDictionaryPointer);
            writer.Write(Unknown_58h);
        }
        public void WriteXml(StringBuilder sb, int indent, string ddsfolder)
        {
            YptXml.StringTag(sb, indent, "Name", YptXml.XmlEscape(Name?.Value ?? ""));
            if (EffectRuleDictionary != null)
            {
                YptXml.OpenTag(sb, indent, "EffectRuleDictionary");
                EffectRuleDictionary.WriteXml(sb, indent + 1);
                YptXml.CloseTag(sb, indent, "EffectRuleDictionary");
            }
            if (EmitterRuleDictionary != null)
            {
                YptXml.OpenTag(sb, indent, "EmitterRuleDictionary");
                EmitterRuleDictionary.WriteXml(sb, indent + 1);
                YptXml.CloseTag(sb, indent, "EmitterRuleDictionary");
            }
            if (ParticleRuleDictionary != null)
            {
                YptXml.OpenTag(sb, indent, "ParticleRuleDictionary");
                ParticleRuleDictionary.WriteXml(sb, indent + 1, ddsfolder);
                YptXml.CloseTag(sb, indent, "ParticleRuleDictionary");
            }
            if (DrawableDictionary != null)
            {
                DrawablePtfxDictionary.WriteXmlNode(DrawableDictionary, sb, indent, ddsfolder, "DrawableDictionary");
            }
            if (TextureDictionary != null)
            {
                TextureDictionary.WriteXmlNode(TextureDictionary, sb, indent, ddsfolder, "TextureDictionary");
            }
        }
        public void ReadXml(XmlNode node, string ddsfolder)
        {
            Name = (string_r)Xml.GetChildInnerText(node, "Name");
            var efnode = node.SelectSingleNode("EffectRuleDictionary");
            if (efnode != null)
            {
                EffectRuleDictionary = new ParticleEffectRuleDictionary();
                EffectRuleDictionary.ReadXml(efnode);
            }
            var emnode = node.SelectSingleNode("EmitterRuleDictionary");
            if (emnode != null)
            {
                EmitterRuleDictionary = new ParticleEmitterRuleDictionary();
                EmitterRuleDictionary.ReadXml(emnode);
            }
            var ptnode = node.SelectSingleNode("ParticleRuleDictionary");
            if (ptnode != null)
            {
                ParticleRuleDictionary = new ParticleRuleDictionary();
                ParticleRuleDictionary.ReadXml(ptnode, ddsfolder);
            }
            var dnode = node.SelectSingleNode("DrawableDictionary");
            if (dnode != null)
            {
                DrawableDictionary = DrawablePtfxDictionary.ReadXmlNode(dnode, ddsfolder);
            }
            var tnode = node.SelectSingleNode("TextureDictionary");
            if (tnode != null)
            {
                TextureDictionary = TextureDictionary.ReadXmlNode(tnode, ddsfolder);
            }

            AssignChildren();
        }
        public static void WriteXmlNode(ParticleEffectsList p, StringBuilder sb, int indent, string ddsfolder, string name = "ParticleEffectsList")
        {
            if (p == null) return;
            YptXml.OpenTag(sb, indent, name);
            p.WriteXml(sb, indent + 1, ddsfolder);
            YptXml.CloseTag(sb, indent, name);
        }
        public static ParticleEffectsList ReadXmlNode(XmlNode node, string ddsfolder)
        {
            if (node == null) return null;
            var p = new ParticleEffectsList();
            p.ReadXml(node, ddsfolder);
            return p;
        }


        public void AssignChildren()
        {
            //assigns any child references on objects that are stored in main dictionaries
            //but, build dictionaries first

            var texdict = new Dictionary<MetaHash, Texture>();
            if (TextureDictionary?.Dict != null)
            {
                foreach (var kvp in TextureDictionary.Dict)
                {
                    texdict[kvp.Key] = kvp.Value;
                }
            }

            var drwdict = new Dictionary<MetaHash, DrawablePtfx>();
            if (DrawableDictionary?.Drawables?.data_items != null)
            {
                var max = Math.Min(DrawableDictionary.Drawables.data_items.Length, (DrawableDictionary.Hashes?.Length ?? 0));
                for (int i = 0; i < max; i++)
                {
                    drwdict[DrawableDictionary.Hashes[i]] = DrawableDictionary.Drawables.data_items[i];
                }
            }

            var ptrdict = new Dictionary<MetaHash, ParticleRule>();
            if (ParticleRuleDictionary?.ParticleRules?.data_items != null)
            {
                foreach (var ptr in ParticleRuleDictionary.ParticleRules.data_items)
                {
                    ptrdict[ptr.NameHash] = ptr;
                }
            }

            var emrdict = new Dictionary<MetaHash, ParticleEmitterRule>();
            if (EmitterRuleDictionary?.EmitterRules?.data_items != null)
            {
                foreach (var emr in EmitterRuleDictionary.EmitterRules.data_items)
                {
                    emrdict[emr.NameHash] = emr;
                }
            }

            var efrdict = new Dictionary<MetaHash, ParticleEffectRule>();
            if (EffectRuleDictionary?.EffectRules?.data_items != null)
            {
                foreach (var efr in EffectRuleDictionary.EffectRules.data_items)
                {
                    efrdict[efr.NameHash] = efr;
                }
            }





            if (EffectRuleDictionary?.EffectRules?.data_items != null)
            {
                foreach (var efr in EffectRuleDictionary.EffectRules.data_items)
                {
                    if (efr?.EventEmitters?.data_items != null)
                    {
                        foreach (var em in efr.EventEmitters.data_items)
                        {
                            if (em == null) continue;
                            var ptrhash = JenkHash.GenHash(em.ParticleRuleName?.Value ?? "");
                            if (ptrdict.TryGetValue(ptrhash, out ParticleRule ptr))
                            {
                                em.ParticleRule = ptr;
                            }
                            else if (ptrhash != 0)
                            { }

                            var emrhash = JenkHash.GenHash(em.EmitterRuleName?.Value ?? "");
                            if (emrdict.TryGetValue(emrhash, out ParticleEmitterRule emr))
                            {
                                em.EmitterRule = emr;
                            }
                            else if (emrhash != 0)
                            { }

                        }
                    }
                }
            }

            if (ParticleRuleDictionary?.ParticleRules?.data_items != null)
            {
                foreach (var ptr in ParticleRuleDictionary.ParticleRules.data_items)
                {
                    if (ptr.Spawner1 != null)
                    {
                        var efrhash = JenkHash.GenHash(ptr.Spawner1.EffectRuleName?.Value ?? "");
                        if (efrdict.TryGetValue(efrhash, out ParticleEffectRule efr))
                        {
                            ptr.Spawner1.EffectRule = efr;
                        }
                        else if (efrhash != 0)
                        { }
                    }
                    if (ptr.Spawner2 != null)
                    {
                        var efrhash = JenkHash.GenHash(ptr.Spawner2.EffectRuleName?.Value ?? "");
                        if (efrdict.TryGetValue(efrhash, out ParticleEffectRule efr))
                        {
                            ptr.Spawner2.EffectRule = efr;
                        }
                        else if (efrhash != 0)
                        { }
                    }
                    if (ptr.Drawables?.data_items != null)
                    {
                        foreach (var pdrw in ptr.Drawables.data_items)
                        {
                            if (drwdict.TryGetValue(pdrw.NameHash, out DrawablePtfx drw))
                            {
                                pdrw.Drawable = drw;
                            }
                            else if (pdrw.NameHash != 0)
                            { }
                        }
                    }
                    if (ptr.ShaderVars?.data_items != null)
                    {
                        foreach (var svar in ptr.ShaderVars.data_items)
                        {
                            if (svar is ParticleShaderVarTexture texvar)
                            {
                                if (texdict.TryGetValue(texvar.TextureNameHash, out Texture tex))
                                {
                                    texvar.Texture = tex;
                                }
                                else if (texvar.TextureNameHash != 0)
                                { }
                            }
                        }
                    }
                }
            }


        }


        public override IResourceBlock[] GetReferences()
        {
            var list = new List<IResourceBlock>(base.GetReferences());
            if (Name != null) list.Add(Name);
            if (TextureDictionary != null) list.Add(TextureDictionary);
            if (DrawableDictionary != null) list.Add(DrawableDictionary);
            if (ParticleRuleDictionary != null) list.Add(ParticleRuleDictionary);
            if (EffectRuleDictionary != null) list.Add(EffectRuleDictionary);
            if (EmitterRuleDictionary != null) list.Add(EmitterRuleDictionary);
            return list.ToArray();
        }
    }


    [TC(typeof(EXP))] public class ParticleRuleDictionary : ResourceSystemBlock
    {
        // pgBase
        // pgDictionaryBase
        // pgDictionary<ptxParticleRule>
        public override long BlockLength => 0x40;

        // structure data
        public uint VFT { get; set; }
        public uint Unknown_4h = 1; // 0x00000001
        public ulong Unknown_8h; // 0x0000000000000000
        public ulong Unknown_10h; // 0x0000000000000000
        public ulong Unknown_18h = 1; // 0x0000000000000001
        public ResourceSimpleList64_s<MetaHash> ParticleRuleNameHashes { get; set; }
        public ResourcePointerList64<ParticleRule> ParticleRules { get; set; }


        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            // read structure data
            VFT = reader.ReadUInt32();
            Unknown_4h = reader.ReadUInt32();
            Unknown_8h = reader.ReadUInt64();
            Unknown_10h = reader.ReadUInt64();
            Unknown_18h = reader.ReadUInt64();
            ParticleRuleNameHashes = reader.ReadBlock<ResourceSimpleList64_s<MetaHash>>();
            ParticleRules = reader.ReadBlock<ResourcePointerList64<ParticleRule>>();

            //if (Unknown_4h != 1)
            //{ }//no hit
            //if (Unknown_8h != 0)
            //{ }//no hit
            //if (Unknown_10h != 0)
            //{ }//no hit
            //if (Unknown_18h != 1)
            //{ }//no hit
        }
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {

            // write structure data
            writer.Write(VFT);
            writer.Write(Unknown_4h);
            writer.Write(Unknown_8h);
            writer.Write(Unknown_10h);
            writer.Write(Unknown_18h);
            writer.WriteBlock(ParticleRuleNameHashes);
            writer.WriteBlock(ParticleRules);
        }
        public void WriteXml(StringBuilder sb, int indent, string ddsfolder)
        {
            if (ParticleRules?.data_items != null)
            {
                var rules = ParticleRules.data_items.ToList();
                rules.Sort((a, b) => { return a.Name?.Value?.CompareTo(b.Name?.Value) ?? ((b.Name?.Value != null) ? 1 : 0); });
                foreach (var r in rules)
                {
                    YptXml.OpenTag(sb, indent, "Item");
                    r.WriteXml(sb, indent + 1, ddsfolder);
                    YptXml.CloseTag(sb, indent, "Item");
                }
            }
        }
        public void ReadXml(XmlNode node, string ddsfolder)
        {
            var rules = new List<ParticleRule>();
            var hashes = new List<MetaHash>();

            var inodes = node.SelectNodes("Item");
            if (inodes != null)
            {
                foreach (XmlNode inode in inodes)
                {
                    var r = new ParticleRule();
                    r.ReadXml(inode, ddsfolder);
                    rules.Add(r);
                }
            }
            rules.Sort((a, b) => { return a.NameHash.Hash.CompareTo(b.NameHash.Hash); });
            foreach (var r in rules)
            {
                hashes.Add(r.NameHash);
            }

            ParticleRuleNameHashes = new ResourceSimpleList64_s<MetaHash>();
            ParticleRuleNameHashes.data_items = hashes.ToArray();
            ParticleRules = new ResourcePointerList64<ParticleRule>();
            ParticleRules.data_items = rules.ToArray();
        }

        public override Tuple<long, IResourceBlock>[] GetParts()
        {
            return new Tuple<long, IResourceBlock>[] {
                new Tuple<long, IResourceBlock>(0x20, ParticleRuleNameHashes),
                new Tuple<long, IResourceBlock>(0x30, ParticleRules)
            };
        }
    }


    [TC(typeof(EXP))] public class ParticleEffectRuleDictionary : ResourceSystemBlock
    {
        // pgBase
        // pgDictionaryBase
        // pgDictionary<ptxEffectRule>
        public override long BlockLength => 0x40;

        // structure data
        public uint VFT { get; set; }
        public uint Unknown_4h = 1; // 0x00000001
        public ulong Unknown_8h; // 0x0000000000000000
        public ulong Unknown_10h; // 0x0000000000000000
        public ulong Unknown_18h = 1; // 0x0000000000000001
        public ResourceSimpleList64_s<MetaHash> EffectRuleNameHashes { get; set; }
        public ResourcePointerList64<ParticleEffectRule> EffectRules { get; set; }

        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            // read structure data
            VFT = reader.ReadUInt32();
            Unknown_4h = reader.ReadUInt32();
            Unknown_8h = reader.ReadUInt64();
            Unknown_10h = reader.ReadUInt64();
            Unknown_18h = reader.ReadUInt64();
            EffectRuleNameHashes = reader.ReadBlock<ResourceSimpleList64_s<MetaHash>>();
            EffectRules = reader.ReadBlock<ResourcePointerList64<ParticleEffectRule>>();

            //if (Unknown_4h != 1)
            //{ }//no hit
            //if (Unknown_8h != 0)
            //{ }//no hit
            //if (Unknown_10h != 0)
            //{ }//no hit
            //if (Unknown_18h != 1)
            //{ }//no hit
        }
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            // write structure data
            writer.Write(VFT);
            writer.Write(Unknown_4h);
            writer.Write(Unknown_8h);
            writer.Write(Unknown_10h);
            writer.Write(Unknown_18h);
            writer.WriteBlock(EffectRuleNameHashes);
            writer.WriteBlock(EffectRules);
        }
        public void WriteXml(StringBuilder sb, int indent)
        {
            if (EffectRules?.data_items != null)
            {
                var rules = EffectRules.data_items.ToList();
                rules.Sort((a, b) => { return a.Name?.Value?.CompareTo(b.Name?.Value) ?? ((b.Name?.Value != null) ? 1 : 0); });
                foreach (var r in rules)
                {
                    YptXml.OpenTag(sb, indent, "Item");
                    r.WriteXml(sb, indent + 1);
                    YptXml.CloseTag(sb, indent, "Item");
                }
            }
        }
        public void ReadXml(XmlNode node)
        {
            var rules = new List<ParticleEffectRule>();
            var hashes = new List<MetaHash>();

            var inodes = node.SelectNodes("Item");
            if (inodes != null)
            {
                foreach (XmlNode inode in inodes)
                {
                    var r = new ParticleEffectRule();
                    r.ReadXml(inode);
                    rules.Add(r);
                }
            }
            rules.Sort((a, b) => { return a.NameHash.Hash.CompareTo(b.NameHash.Hash); });
            foreach (var r in rules)
            {
                hashes.Add(r.NameHash);
            }

            EffectRuleNameHashes = new ResourceSimpleList64_s<MetaHash>();
            EffectRuleNameHashes.data_items = hashes.ToArray();
            EffectRules = new ResourcePointerList64<ParticleEffectRule>();
            EffectRules.data_items = rules.ToArray();
        }

        public override Tuple<long, IResourceBlock>[] GetParts()
        {
            return new Tuple<long, IResourceBlock>[] {
                new Tuple<long, IResourceBlock>(0x20, EffectRuleNameHashes),
                new Tuple<long, IResourceBlock>(0x30, EffectRules)
            };
        }
    }


    [TC(typeof(EXP))] public class ParticleEmitterRuleDictionary : ResourceSystemBlock
    {
        // pgBase
        // pgDictionaryBase
        // pgDictionary<ptxEmitterRule>
        public override long BlockLength => 0x40;

        // structure data
        public uint VFT { get; set; }
        public uint Unknown_4h = 1; // 0x00000001
        public ulong Unknown_8h; // 0x0000000000000000
        public ulong Unknown_10h; // 0x0000000000000000
        public ulong Unknown_18h = 1; // 0x0000000000000001
        public ResourceSimpleList64_s<MetaHash> EmitterRuleNameHashes { get; set; }
        public ResourcePointerList64<ParticleEmitterRule> EmitterRules { get; set; }


        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            // read structure data
            VFT = reader.ReadUInt32();
            Unknown_4h = reader.ReadUInt32();
            Unknown_8h = reader.ReadUInt64();
            Unknown_10h = reader.ReadUInt64();
            Unknown_18h = reader.ReadUInt64();
            EmitterRuleNameHashes = reader.ReadBlock<ResourceSimpleList64_s<MetaHash>>();
            EmitterRules = reader.ReadBlock<ResourcePointerList64<ParticleEmitterRule>>();


            //if (Unknown_4h != 1)
            //{ }//no hit
            //if (Unknown_8h != 0)
            //{ }//no hit
            //if (Unknown_10h != 0)
            //{ }//no hit
            //if (Unknown_18h != 1)
            //{ }//no hit
        }
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {

            // write structure data
            writer.Write(VFT);
            writer.Write(Unknown_4h);
            writer.Write(Unknown_8h);
            writer.Write(Unknown_10h);
            writer.Write(Unknown_18h);
            writer.WriteBlock(EmitterRuleNameHashes);
            writer.WriteBlock(EmitterRules);
        }
        public void WriteXml(StringBuilder sb, int indent)
        {
            if (EmitterRules?.data_items != null)
            {
                var rules = EmitterRules.data_items.ToList();
                rules.Sort((a, b) => { return a.Name?.Value?.CompareTo(b.Name?.Value) ?? ((b.Name?.Value != null) ? 1 : 0); });
                foreach (var r in rules)
                {
                    YptXml.OpenTag(sb, indent, "Item");
                    r.WriteXml(sb, indent + 1);
                    YptXml.CloseTag(sb, indent, "Item");
                }
            }
        }
        public void ReadXml(XmlNode node)
        {
            var rules = new List<ParticleEmitterRule>();
            var hashes = new List<MetaHash>();

            var inodes = node.SelectNodes("Item");
            if (inodes != null)
            {
                foreach (XmlNode inode in inodes)
                {
                    var r = new ParticleEmitterRule();
                    r.ReadXml(inode);
                    rules.Add(r);
                }
            }
            rules.Sort((a, b) => { return a.NameHash.Hash.CompareTo(b.NameHash.Hash); });
            foreach (var r in rules)
            {
                hashes.Add(r.NameHash);
            }

            EmitterRuleNameHashes = new ResourceSimpleList64_s<MetaHash>();
            EmitterRuleNameHashes.data_items = hashes.ToArray();
            EmitterRules = new ResourcePointerList64<ParticleEmitterRule>();
            EmitterRules.data_items = rules.ToArray();
        }

        public override Tuple<long, IResourceBlock>[] GetParts()
        {
            return new Tuple<long, IResourceBlock>[] {
                new Tuple<long, IResourceBlock>(0x20, EmitterRuleNameHashes),
                new Tuple<long, IResourceBlock>(0x30, EmitterRules)
            };
        }
    }








    [TC(typeof(EXP))] public class ParticleRule : ResourceSystemBlock
    {
        // pgBase
        // pgBaseRefCounted
        // ptxParticleRule
        public override long BlockLength => 0x240;

        // structure data
        public uint VFT { get; set; }
        public uint Unknown_4h = 1; // 0x00000001
        public ulong Unknown_8h; // 0x0000000000000000
        public uint Unknown_10h { get; set; }   // 2, 3, 4, 5, 6, 7, 10, 21
        public uint Unknown_14h; //0x00000000
        public ulong Unknown_18h; // 0x0000000000000000
        public ParticleEffectSpawner Spawner1 { get; set; }
        public ParticleEffectSpawner Spawner2 { get; set; }
        public uint Unknown_100h { get; set; }  // 0, 1, 2
        public uint Unknown_104h { get; set; }  // 0, 1, 7
        public uint Unknown_108h { get; set; }  // 0, 1, 2
        public uint Unknown_10Ch { get; set; }  // eg. 0x00010100
        public uint Unknown_110h; // 0x00000000
        public float Unknown_114h { get; set; } = 1.0f;
        public uint Unknown_118h { get; set; } //index/id
        public uint Unknown_11Ch { get; set; } //index/id
        public ulong NamePointer { get; set; }
        public ResourcePointerList64<ParticleBehaviour> BehaviourList1 { get; set; }
        public ResourcePointerList64<ParticleBehaviour> BehaviourList2 { get; set; }
        public ResourcePointerList64<ParticleBehaviour> BehaviourList3 { get; set; }
        public ResourcePointerList64<ParticleBehaviour> BehaviourList4 { get; set; }
        public ResourcePointerList64<ParticleBehaviour> BehaviourList5 { get; set; }
        public ulong Unknown_178h; // 0x0000000000000000
        public ulong Unknown_180h; // 0x0000000000000000
        public ResourceSimpleList64<ParticleRuleUnknownItem> UnknownList1 { get; set; }
        public ulong Unknown_198h; // 0x0000000000000000
        public ulong Unknown_1A0h; // 0x0000000000000000
        public ulong Unknown_1A8h; // 0x0000000000000000
        public uint VFT2 { get; set; } = 0x40605c50; // 0x40605c50, 0x40607c70
        public uint Unknown_1B4h = 1; // 0x00000001
        public ulong FxcFilePointer { get; set; }
        public ulong FxcTechniquePointer { get; set; }
        public ulong Unknown_1C8h; // 0x0000000000000000
        public uint Unknown_1D0h { get; set; } //index/id
        public uint Unknown_1D4h; // 0x00000000
        public uint VFT3 { get; set; } = 0x40605b48; // 0x40605b48, 0x40607b68
        public uint Unknown_1DCh = 1; // 0x00000001
        public uint Unknown_1E0h { get; set; }  // 0, 4
        public uint Unknown_1E4h { get; set; }  // 0, 1
        public uint Unknown_1E8h { get; set; }  // eg. 0x00000101
        public uint Unknown_1ECh { get; set; }  // 0, 1
        public ResourcePointerList64<ParticleShaderVar> ShaderVars { get; set; }
        public ulong Unknown_200h = 1; // 0x0000000000000001
        public MetaHash FxcFileHash { get; set; } // ptfx_sprite, ptfx_trail
        public uint Unknown_20Ch; // 0x00000000
        public ResourceSimpleList64<ParticleDrawable> Drawables { get; set; }
        public uint Unknown_220h { get; set; }  // eg. 0x00000202
        public uint Unknown_224h; // 0x00000000
        public ulong Unknown_228h; // 0x0000000000000000
        public ulong Unknown_230h; // 0x0000000000000000
        public ulong Unknown_238h; // 0x0000000000000000

        // reference data
        public string_r Name { get; set; }
        public MetaHash NameHash { get; set; }
        public string_r FxcFile { get; set; } // ptfx_sprite, ptfx_trail
        public string_r FxcTechnique { get; set; }

        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            #region read data

            // read structure data
            VFT = reader.ReadUInt32();
            Unknown_4h = reader.ReadUInt32();
            Unknown_8h = reader.ReadUInt64();
            Unknown_10h = reader.ReadUInt32();
            Unknown_14h = reader.ReadUInt32();
            Unknown_18h = reader.ReadUInt64();
            Spawner1 = reader.ReadBlock<ParticleEffectSpawner>();
            Spawner2 = reader.ReadBlock<ParticleEffectSpawner>();
            Unknown_100h = reader.ReadUInt32();
            Unknown_104h = reader.ReadUInt32();
            Unknown_108h = reader.ReadUInt32();
            Unknown_10Ch = reader.ReadUInt32();
            Unknown_110h = reader.ReadUInt32();
            Unknown_114h = reader.ReadSingle();
            Unknown_118h = reader.ReadUInt32();
            Unknown_11Ch = reader.ReadUInt32();
            NamePointer = reader.ReadUInt64();
            BehaviourList1 = reader.ReadBlock<ResourcePointerList64<ParticleBehaviour>>();
            BehaviourList2 = reader.ReadBlock<ResourcePointerList64<ParticleBehaviour>>();
            BehaviourList3 = reader.ReadBlock<ResourcePointerList64<ParticleBehaviour>>();
            BehaviourList4 = reader.ReadBlock<ResourcePointerList64<ParticleBehaviour>>();
            BehaviourList5 = reader.ReadBlock<ResourcePointerList64<ParticleBehaviour>>();
            Unknown_178h = reader.ReadUInt64();
            Unknown_180h = reader.ReadUInt64();
            UnknownList1 = reader.ReadBlock<ResourceSimpleList64<ParticleRuleUnknownItem>>();
            Unknown_198h = reader.ReadUInt64();
            Unknown_1A0h = reader.ReadUInt64();
            Unknown_1A8h = reader.ReadUInt64();
            VFT2 = reader.ReadUInt32();
            Unknown_1B4h = reader.ReadUInt32();
            FxcFilePointer = reader.ReadUInt64();
            FxcTechniquePointer = reader.ReadUInt64();
            Unknown_1C8h = reader.ReadUInt64();
            Unknown_1D0h = reader.ReadUInt32();
            Unknown_1D4h = reader.ReadUInt32();
            VFT3 = reader.ReadUInt32();
            Unknown_1DCh = reader.ReadUInt32();
            Unknown_1E0h = reader.ReadUInt32();
            Unknown_1E4h = reader.ReadUInt32();
            Unknown_1E8h = reader.ReadUInt32();
            Unknown_1ECh = reader.ReadUInt32();
            ShaderVars = reader.ReadBlock<ResourcePointerList64<ParticleShaderVar>>();
            Unknown_200h = reader.ReadUInt64();
            FxcFileHash = reader.ReadUInt32();
            Unknown_20Ch = reader.ReadUInt32();
            Drawables = reader.ReadBlock<ResourceSimpleList64<ParticleDrawable>>();
            Unknown_220h = reader.ReadUInt32();
            Unknown_224h = reader.ReadUInt32();
            Unknown_228h = reader.ReadUInt64();
            Unknown_230h = reader.ReadUInt64();
            Unknown_238h = reader.ReadUInt64();

            // read reference data
            Name = reader.ReadBlockAt<string_r>(NamePointer);
            FxcFile = reader.ReadBlockAt<string_r>(FxcFilePointer);
            FxcTechnique = reader.ReadBlockAt<string_r>(FxcTechniquePointer);

            #endregion


            if (!string.IsNullOrEmpty(Name?.Value))
            {
                JenkIndex.Ensure(Name.Value);
            }

            if ((Drawables?.data_items?.Length ?? 0) != 0)
            { }


            #region test

            //var bl1 = BehaviourList1?.data_items?.ToList() ?? new List<ParticleBehaviour>();
            //var bl2 = BehaviourList2?.data_items?.ToList() ?? new List<ParticleBehaviour>();
            //var bl3 = BehaviourList3?.data_items?.ToList() ?? new List<ParticleBehaviour>();
            //var bl4 = BehaviourList4?.data_items?.ToList() ?? new List<ParticleBehaviour>();
            //var bl5 = BehaviourList5?.data_items?.ToList() ?? new List<ParticleBehaviour>();
            //if (bl2.Count != bl3.Count)
            //{ }//no hit
            //foreach (var b in bl1)
            //{
            //    var t = b.Type;
            //    var il2 = bl2.Contains(b);
            //    var il3 = bl3.Contains(b);
            //    var il4 = bl4.Contains(b);
            //    var il5 = bl5.Contains(b);
            //    var render = false;
            //    var extra = false;
            //    var extra2 = false;
            //    switch (t)
            //    {
            //        case ParticleBehaviourType.Sprite:
            //        case ParticleBehaviourType.Model:
            //        case ParticleBehaviourType.Trail:
            //            render = true;
            //            break;
            //    }
            //    switch (t)
            //    {
            //        case ParticleBehaviourType.Collision:
            //        case ParticleBehaviourType.Light:
            //        case ParticleBehaviourType.Decal:
            //        case ParticleBehaviourType.ZCull:
            //        case ParticleBehaviourType.Trail:
            //        case ParticleBehaviourType.FogVolume:
            //        case ParticleBehaviourType.River:
            //        case ParticleBehaviourType.DecalPool:
            //        case ParticleBehaviourType.Liquid:
            //            extra = true;
            //            break;
            //    }
            //    switch (t)
            //    {
            //        case ParticleBehaviourType.Sprite:
            //        case ParticleBehaviourType.Model:
            //        case ParticleBehaviourType.Trail:
            //        case ParticleBehaviourType.FogVolume:
            //            extra2 = true;
            //            break;
            //    }
            //    if (il2 != il3)
            //    { }//no hit
            //    if (il2 == render)
            //    { }//no hit
            //    if (il4 != extra)
            //    { }//no hit
            //    if (il5 != extra2)
            //    { }//no hit
            //}

            //var blc1 = BehaviourList1?.data_items?.Length ?? 0;
            //var blc2 = BehaviourList2?.data_items?.Length ?? 0;
            //for (int i = 0; i < blc2; i++)
            //{
            //    var b = BehaviourList2.data_items[i];
            //    if (!bl1.Contains(b))
            //    { }//no hit
            //}
            //var blc3 = BehaviourList3?.data_items?.Length ?? 0;
            //for (int i = 0; i < blc3; i++)
            //{
            //    var b = BehaviourList3.data_items[i];
            //    if (!bl1.Contains(b))
            //    { }//no hit
            //}
            //var blc4 = BehaviourList4?.data_items?.Length ?? 0;
            //for (int i = 0; i < blc4; i++)
            //{
            //    var b = BehaviourList4.data_items[i];
            //    if (!bl1.Contains(b))
            //    { }//no hit
            //}
            //var blc5 = BehaviourList5?.data_items?.Length ?? 0;
            //for (int i = 0; i < blc5; i++)
            //{
            //    var b = BehaviourList5.data_items[i];
            //    if (!bl1.Contains(b))
            //    { }//no hit
            //}




            //if (Unknown_4h != 1)
            //{ }//no hit
            //if (Unknown_8h != 0)
            //{ }//no hit
            //switch (Unknown_10h)
            //{
            //    case 4:
            //    case 2:
            //    case 3:
            //    case 6:
            //    case 7:
            //    case 5:
            //    case 10:
            //    case 21:
            //        break;
            //    default:
            //        break;//no hit
            //}
            //if (Unknown_14h != 0)
            //{ }//no hit
            //if (Unknown_18h != 0)
            //{ }//no hit
            //switch (Unknown_100h)
            //{
            //    case 2:
            //    case 0:
            //    case 1:
            //        break;
            //    default:
            //        break;//no hit
            //}
            //switch (Unknown_104h)
            //{
            //    case 0:
            //    case 1:
            //    case 7:
            //        break;
            //    default:
            //        break;//no hit
            //}
            //switch (Unknown_108h)
            //{
            //    case 2:
            //    case 1:
            //    case 0:
            //        break;
            //    default:
            //        break;//no hit
            //}
            //switch (Unknown_10Ch)
            //{
            //    case 0x00010100:
            //    case 0x00010000:
            //    case 0x00010101:
            //        break;
            //    default:
            //        break;//no hit
            //}
            //if (Unknown_110h != 0)
            //{ }//no hit
            //if (Unknown_114h != 1.0f)
            //{ }//no hit
            //switch (Unknown_118h)
            //{
            //    case 0:
            //    case 8:
            //    case 13:
            //    case 15:
            //    case 16:
            //    case 1:
            //    case 20:
            //    case 9:
            //    case 5:
            //    case 11:
            //    case 22:
            //    case 2:
            //    case 12:
            //    case 10:
            //    case 6:
            //    case 14:
            //    case 23:
            //    case 3:
            //    case 19:
            //    case 18:
            //    case 4:
            //    case 7:
            //    case 25:
            //    case 26:
            //    case 21:
            //        break;
            //    default:
            //        break;//no hit
            //}
            //switch (Unknown_11Ch)
            //{
            //    case 2:
            //    case 3:
            //    case 14:
            //    case 23:
            //    case 48:
            //    case 22:
            //    case 1:
            //    case 12:
            //    case 11:
            //    case 0:
            //    case 25:
            //    case 7:
            //    case 8:
            //    case 21:
            //    case 15:
            //    case 28:
            //    case 18:
            //    case 20:
            //    case 33:
            //    case 5:
            //    case 26:
            //    case 24:
            //    case 9:
            //    case 35:
            //    case 10:
            //    case 38:
            //    case 27:
            //    case 13:
            //    case 16:
            //    case 17:
            //    case 36:
            //    case 4:
            //    case 19:
            //    case 31:
            //    case 47:
            //    case 32:
            //    case 34:
            //    case 6:
            //    case 30:
            //        break;
            //    default:
            //        break;//no hit
            //}
            //if (Unknown_178h != 0)
            //{ }//no hit
            //if (Unknown_180h != 0)
            //{ }//no hit
            //if (Unknown_198h != 0)
            //{ }//no hit
            //if (Unknown_1A0h != 0)
            //{ }//no hit
            //if (Unknown_1A8h != 0)
            //{ }//no hit
            //switch (VFT2)
            //{
            //    case 0x40605c50:
            //    case 0x40607c70:
            //        break;
            //    default:
            //        break;//no hit
            //}
            //if (Unknown_1B4h != 1)
            //{ }//no hit
            //if (Unknown_1C8h != 0)
            //{ }//no hit
            //switch (Unknown_1D0h)
            //{
            //    case 5:
            //    case 2:
            //    case 8:
            //    case 6:
            //    case 13:
            //    case 16:
            //    case 20:
            //    case 3:
            //    case 12:
            //    case 1:
            //    case 14:
            //    case 27:
            //    case 21:
            //    case 9:
            //    case 4:
            //    case 19:
            //        break;
            //    default:
            //        break;//no hit
            //}
            //if (Unknown_1D4h != 0)
            //{ }//no hit
            //switch (VFT3)
            //{
            //    case 0x40605b48:
            //    case 0x40607b68:
            //        break;
            //    default:
            //        break;//no hit
            //}
            //if (Unknown_1DCh != 1)
            //{ }//no hit
            //switch (Unknown_1E0h)
            //{
            //    case 0:
            //    case 4:
            //        break;
            //    default:
            //        break;//no hit
            //}
            //switch (Unknown_1E4h)
            //{
            //    case 0:
            //    case 1:
            //        break;
            //    default:
            //        break;//no hit
            //}
            //switch (Unknown_1E8h)
            //{
            //    case 0x00000101:
            //    case 1:
            //    case 0x00010001:
            //    case 0x01000000:
            //    case 0x00000100:
            //    case 0x01000100:
            //    case 0:
            //        break;
            //    default:
            //        break;//no hit
            //}
            //switch (Unknown_1ECh)
            //{
            //    case 0:
            //    case 1:
            //        break;
            //    default:
            //        break;//no hit
            //}
            //if (Unknown_200h != 1)
            //{ }//no hit
            //switch (FxcFileHash) // .fxc shader file name
            //{
            //    case 0x0eb0d762: // ptfx_sprite
            //    case 0xe7b0585f: // ptfx_trail
            //        break;
            //    default:
            //        break;//no hit
            //}
            //if (FxcFileHash != JenkHash.GenHash(FxcFile?.ToString() ?? ""))
            //{ }//no hit
            //if (Unknown_20Ch != 0)
            //{ }//no hit
            //switch (Unknown_220h)
            //{
            //    case 1:
            //    case 2:
            //    case 0:
            //    case 0x00000202:
            //    case 0x00000102:
            //    case 0x00000101:
            //    case 3:
            //    case 4:
            //    case 0x00000100:
            //    case 0x00000103:
            //        break;
            //    default:
            //        break;//no hit
            //}
            //if (Unknown_224h != 0)
            //{ }//no hit
            //if (Unknown_228h != 0)
            //{ }//no hit
            //if (Unknown_230h != 0)
            //{ }//no hit
            //if (Unknown_238h != 0)
            //{ }//no hit
            #endregion
        }
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            // update structure data
            NamePointer = (ulong)(Name != null ? Name.FilePosition : 0);
            FxcFilePointer = (ulong)(FxcFile != null ? FxcFile.FilePosition : 0);
            FxcTechniquePointer = (ulong)(FxcTechnique != null ? FxcTechnique.FilePosition : 0);

            // write structure data
            writer.Write(VFT);
            writer.Write(Unknown_4h);
            writer.Write(Unknown_8h);
            writer.Write(Unknown_10h);
            writer.Write(Unknown_14h);
            writer.Write(Unknown_18h);
            writer.WriteBlock(Spawner1);
            writer.WriteBlock(Spawner2);
            writer.Write(Unknown_100h);
            writer.Write(Unknown_104h);
            writer.Write(Unknown_108h);
            writer.Write(Unknown_10Ch);
            writer.Write(Unknown_110h);
            writer.Write(Unknown_114h);
            writer.Write(Unknown_118h);
            writer.Write(Unknown_11Ch);
            writer.Write(NamePointer);
            writer.WriteBlock(BehaviourList1);
            writer.WriteBlock(BehaviourList2);
            writer.WriteBlock(BehaviourList3);
            writer.WriteBlock(BehaviourList4);
            writer.WriteBlock(BehaviourList5);
            writer.Write(Unknown_178h);
            writer.Write(Unknown_180h);
            writer.WriteBlock(UnknownList1);
            writer.Write(Unknown_198h);
            writer.Write(Unknown_1A0h);
            writer.Write(Unknown_1A8h);
            writer.Write(VFT2);
            writer.Write(Unknown_1B4h);
            writer.Write(FxcFilePointer);
            writer.Write(FxcTechniquePointer);
            writer.Write(Unknown_1C8h);
            writer.Write(Unknown_1D0h);
            writer.Write(Unknown_1D4h);
            writer.Write(VFT3);
            writer.Write(Unknown_1DCh);
            writer.Write(Unknown_1E0h);
            writer.Write(Unknown_1E4h);
            writer.Write(Unknown_1E8h);
            writer.Write(Unknown_1ECh);
            writer.WriteBlock(ShaderVars);
            writer.Write(Unknown_200h);
            writer.Write(FxcFileHash);
            writer.Write(Unknown_20Ch);
            writer.WriteBlock(Drawables);
            writer.Write(Unknown_220h);
            writer.Write(Unknown_224h);
            writer.Write(Unknown_228h);
            writer.Write(Unknown_230h);
            writer.Write(Unknown_238h);
        }
        public void WriteXml(StringBuilder sb, int indent, string ddsfolder)
        {
            YptXml.StringTag(sb, indent, "Name", YptXml.XmlEscape(Name?.Value ?? ""));
            YptXml.StringTag(sb, indent, "FxcFile", YptXml.XmlEscape(FxcFile?.Value ?? ""));
            YptXml.StringTag(sb, indent, "FxcTechnique", YptXml.XmlEscape(FxcTechnique?.Value ?? ""));
            YptXml.ValueTag(sb, indent, "Unknown10", Unknown_10h.ToString());
            YptXml.ValueTag(sb, indent, "Unknown100", Unknown_100h.ToString());
            YptXml.ValueTag(sb, indent, "Unknown104", Unknown_104h.ToString());
            YptXml.ValueTag(sb, indent, "Unknown108", Unknown_108h.ToString());
            YptXml.ValueTag(sb, indent, "Unknown10C", YptXml.UintString(Unknown_10Ch));
            YptXml.ValueTag(sb, indent, "Unknown118", Unknown_118h.ToString());
            YptXml.ValueTag(sb, indent, "Unknown11C", Unknown_11Ch.ToString());
            YptXml.ValueTag(sb, indent, "Unknown1D0", Unknown_1D0h.ToString());
            YptXml.ValueTag(sb, indent, "Unknown1E0", Unknown_1E0h.ToString());
            YptXml.ValueTag(sb, indent, "Unknown1E4", Unknown_1E4h.ToString());
            YptXml.ValueTag(sb, indent, "Unknown1E8", YptXml.UintString(Unknown_1E8h));
            YptXml.ValueTag(sb, indent, "Unknown1EC", Unknown_1ECh.ToString());
            YptXml.ValueTag(sb, indent, "Unknown220", YptXml.UintString(Unknown_220h));
            if (Spawner1 != null)
            {
                YptXml.OpenTag(sb, indent, "Spawner1");
                Spawner1.WriteXml(sb, indent + 1);
                YptXml.CloseTag(sb, indent, "Spawner1");
            }
            if (Spawner2 != null)
            {
                YptXml.OpenTag(sb, indent, "Spawner2");
                Spawner2.WriteXml(sb, indent + 1);
                YptXml.CloseTag(sb, indent, "Spawner2");
            }
            if (BehaviourList1?.data_items?.Length > 0)
            {
                YptXml.WriteItemArray(sb, BehaviourList1.data_items, indent, "Behaviours");
            }
            if (UnknownList1?.data_items?.Length > 0)
            {
                YptXml.WriteItemArray(sb, UnknownList1.data_items, indent, "UnknownList1");
            }
            if (ShaderVars?.data_items?.Length > 0)
            {
                YptXml.WriteItemArray(sb, ShaderVars.data_items, indent, "ShaderVars");
            }
            if (Drawables?.data_items?.Length > 0)
            {
                YptXml.WriteItemArray(sb, Drawables.data_items, indent, "Drawables");
            }
        }
        public void ReadXml(XmlNode node, string ddsfolder)
        {
            Name = (string_r)Xml.GetChildInnerText(node, "Name"); if (Name.Value == null) Name = null;
            NameHash = JenkHash.GenHash(Name?.Value ?? "");
            FxcFile = (string_r)Xml.GetChildInnerText(node, "FxcFile"); if (FxcFile.Value == null) FxcFile = null;
            FxcTechnique = (string_r)Xml.GetChildInnerText(node, "FxcTechnique"); if (FxcTechnique.Value == null) FxcTechnique = null;
            Unknown_10h = Xml.GetChildUIntAttribute(node, "Unknown10");
            Unknown_100h = Xml.GetChildUIntAttribute(node, "Unknown100");
            Unknown_104h = Xml.GetChildUIntAttribute(node, "Unknown104");
            Unknown_108h = Xml.GetChildUIntAttribute(node, "Unknown108");
            Unknown_10Ch = Xml.GetChildUIntAttribute(node, "Unknown10C");
            Unknown_118h = Xml.GetChildUIntAttribute(node, "Unknown118");
            Unknown_11Ch = Xml.GetChildUIntAttribute(node, "Unknown11C");
            Unknown_1D0h = Xml.GetChildUIntAttribute(node, "Unknown1D0");
            Unknown_1E0h = Xml.GetChildUIntAttribute(node, "Unknown1E0");
            Unknown_1E4h = Xml.GetChildUIntAttribute(node, "Unknown1E4");
            Unknown_1E8h = Xml.GetChildUIntAttribute(node, "Unknown1E8");
            Unknown_1ECh = Xml.GetChildUIntAttribute(node, "Unknown1EC");
            Unknown_220h = Xml.GetChildUIntAttribute(node, "Unknown220");
            Spawner1 = new ParticleEffectSpawner();
            Spawner1.ReadXml(node.SelectSingleNode("Spawner1"));
            Spawner2 = new ParticleEffectSpawner();
            Spawner2.ReadXml(node.SelectSingleNode("Spawner2"));



            var bnode = node.SelectSingleNode("Behaviours");
            var blist = new List<ParticleBehaviour>();
            if (bnode != null)
            {
                var inodes = bnode.SelectNodes("Item");
                if (inodes?.Count > 0)
                {
                    foreach (XmlNode inode in inodes)
                    {
                        var b = ParticleBehaviour.ReadXmlNode(inode);
                        blist.Add(b);
                    }
                }
            }
            BuildBehaviours(blist);




            UnknownList1 = new ResourceSimpleList64<ParticleRuleUnknownItem>();
            UnknownList1.data_items = XmlMeta.ReadItemArrayNullable<ParticleRuleUnknownItem>(node, "UnknownList1");


            ResourcePointerList64<ParticleShaderVar> readShaderVars(string name)
            {
                var sha = new ResourcePointerList64<ParticleShaderVar>();
                var snode = node.SelectSingleNode(name);
                if (snode != null)
                {
                    var inodes = snode.SelectNodes("Item");
                    if (inodes?.Count > 0)
                    {
                        var slist = new List<ParticleShaderVar>();
                        foreach (XmlNode inode in inodes)
                        {
                            var s = ParticleShaderVar.ReadXmlNode(inode);
                            slist.Add(s);
                        }
                        sha.data_items = slist.ToArray();
                    }
                }
                return sha;
            }
            ShaderVars = readShaderVars("ShaderVars");


            Drawables = new ResourceSimpleList64<ParticleDrawable>();
            Drawables.data_items = XmlMeta.ReadItemArrayNullable<ParticleDrawable>(node, "Drawables");
        }


        public void BuildBehaviours(List<ParticleBehaviour> blist)
        {
            var blist2 = new List<ParticleBehaviour>();
            var blist3 = new List<ParticleBehaviour>();
            var blist4 = new List<ParticleBehaviour>();
            var blist5 = new List<ParticleBehaviour>();

            foreach (var b in blist)
            {
                if (b == null) continue;
                var render = false;
                var extra = false;
                var extra2 = false;
                switch (b.Type)
                {
                    case ParticleBehaviourType.Sprite:
                    case ParticleBehaviourType.Model:
                    case ParticleBehaviourType.Trail:
                        render = true;
                        break;
                }
                switch (b.Type)
                {
                    case ParticleBehaviourType.Collision:
                    case ParticleBehaviourType.Light:
                    case ParticleBehaviourType.Decal:
                    case ParticleBehaviourType.ZCull:
                    case ParticleBehaviourType.Trail:
                    case ParticleBehaviourType.FogVolume:
                    case ParticleBehaviourType.River:
                    case ParticleBehaviourType.DecalPool:
                    case ParticleBehaviourType.Liquid:
                        extra = true;
                        break;
                }
                switch (b.Type)
                {
                    case ParticleBehaviourType.Sprite:
                    case ParticleBehaviourType.Model:
                    case ParticleBehaviourType.Trail:
                    case ParticleBehaviourType.FogVolume:
                        extra2 = true;
                        break;
                }
                if (!render)
                {
                    blist2.Add(b);
                    blist3.Add(b);
                }
                if (extra)
                {
                    blist4.Add(b);
                }
                if (extra2)
                {
                    blist5.Add(b);
                }
            }

            BehaviourList1 = new ResourcePointerList64<ParticleBehaviour>();
            BehaviourList1.data_items = blist.ToArray();
            BehaviourList2 = new ResourcePointerList64<ParticleBehaviour>();
            BehaviourList2.data_items = blist2.ToArray();
            BehaviourList3 = new ResourcePointerList64<ParticleBehaviour>();
            BehaviourList3.data_items = blist3.ToArray();
            BehaviourList4 = new ResourcePointerList64<ParticleBehaviour>();
            BehaviourList4.data_items = blist4.ToArray();
            BehaviourList5 = new ResourcePointerList64<ParticleBehaviour>();
            BehaviourList5.data_items = blist5.ToArray();


        }


        public override IResourceBlock[] GetReferences()
        {
            var list = new List<IResourceBlock>();
            if (Name != null) list.Add(Name);
            if (FxcFile != null) list.Add(FxcFile);
            if (FxcTechnique != null) list.Add(FxcTechnique);
            return list.ToArray();
        }

        public override Tuple<long, IResourceBlock>[] GetParts()
        {
            return new Tuple<long, IResourceBlock>[] {
                new Tuple<long, IResourceBlock>(88, Spawner1),
                new Tuple<long, IResourceBlock>(96, Spawner2),
                new Tuple<long, IResourceBlock>(0x128, BehaviourList1),
                new Tuple<long, IResourceBlock>(0x138, BehaviourList2),
                new Tuple<long, IResourceBlock>(0x148, BehaviourList3),
                new Tuple<long, IResourceBlock>(0x158, BehaviourList4),
                new Tuple<long, IResourceBlock>(0x168, BehaviourList5),
                new Tuple<long, IResourceBlock>(0x188, UnknownList1),
                new Tuple<long, IResourceBlock>(0x1F0, ShaderVars),
                new Tuple<long, IResourceBlock>(0x210, Drawables)
            };
        }

        public override string ToString()
        {
            return Name?.ToString() ?? base.ToString();
        }
    }


    [TC(typeof(EXP))] public class ParticleRuleUnknownItem : ResourceSystemBlock, IMetaXmlItem
    {
        public override long BlockLength => 0x58;




        // structure data
        public PsoChar32 Name { get; set; }
        public ulong Unknown_20h; // 0x0000000000000000
        public ulong Unknown_28h; // 0x0000000000000000
        public ulong Unknown_30h; // 0x0000000000000000
        public ulong Unknown_38h; // 0x0000000000000000
        public ResourceSimpleList64_s<MetaHash> Unknown_40h { get; set; }
        public uint Unknown_50h { get; set; }
        public uint Unknown_54h; // 0x00000000

        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            // read structure data
            Name = reader.ReadStruct<PsoChar32>();
            Unknown_20h = reader.ReadUInt64();
            Unknown_28h = reader.ReadUInt64();
            Unknown_30h = reader.ReadUInt64();
            Unknown_38h = reader.ReadUInt64();
            Unknown_40h = reader.ReadBlock<ResourceSimpleList64_s<MetaHash>>();
            Unknown_50h = reader.ReadUInt32();
            Unknown_54h = reader.ReadUInt32();

            //if (Name.ToString() != "Bias Link Set_00")
            //{ }//no hit
            //if (Unknown_20h != 0)
            //{ }//no hit
            //if (Unknown_28h != 0)
            //{ }//no hit
            //if (Unknown_30h != 0)
            //{ }//no hit
            //if (Unknown_38h != 0)
            //{ }//no hit
            switch (Unknown_50h) // ..index?
            {
                case 0x000000f6:
                case 0x000000f7:
                case 0x000000d5:
                case 0x000000f0:
                case 0x000000f1:
                case 0x000000f2:
                case 0x000000f3:
                case 0x000000f4:
                case 0x000000ed:
                case 0x000000a6:
                case 0x000000a7:
                case 0x000000e7:
                case 0x00000081:
                case 0x00000082:
                case 0x00000083:
                case 0x000000e5:
                case 0x000000e6:
                case 0x000000e8:
                case 0x000000e9:
                case 0x000000ea:
                    break;
                default:
                    break;//more
            }
            //if (Unknown_54h != 0)
            //{ }//no hit
        }
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            // write structure data
            writer.WriteStruct(Name);
            writer.Write(Unknown_20h);
            writer.Write(Unknown_28h);
            writer.Write(Unknown_30h);
            writer.Write(Unknown_38h);
            writer.WriteBlock(Unknown_40h);
            writer.Write(Unknown_50h);
            writer.Write(Unknown_54h);
        }
        public void WriteXml(StringBuilder sb, int indent)
        {
            YptXml.StringTag(sb, indent, "Name", YptXml.XmlEscape(Name.ToString()));
            YptXml.ValueTag(sb, indent, "Unknown50", Unknown_50h.ToString());
            YptXml.WriteHashItemArray(sb, Unknown_40h?.data_items, indent, "Unknown40");
        }
        public void ReadXml(XmlNode node)
        {
            Name = new PsoChar32(Xml.GetChildInnerText(node, "Name"));
            Unknown_50h = Xml.GetChildUIntAttribute(node, "Unknown50");
            Unknown_40h = new ResourceSimpleList64_s<MetaHash>();
            Unknown_40h.data_items = XmlMeta.ReadHashItemArray(node, "Unknown40");
        }

        public override Tuple<long, IResourceBlock>[] GetParts()
        {
            return new Tuple<long, IResourceBlock>[] {
                new Tuple<long, IResourceBlock>(0x40, Unknown_40h)
            };
        }

        public override string ToString()
        {
            var n = Name.ToString();
            return (!string.IsNullOrEmpty(n)) ? n : base.ToString();
        }

    }


    [TC(typeof(EXP))] public class ParticleEffectSpawner : ResourceSystemBlock
    {
        // pgBase
        // ptxEffectSpawner
        public override long BlockLength => 0x70;

        // structure data
        public uint VFT { get; set; }
        public uint Unknown_4h = 1; // 0x00000001
        public ulong Unknown_8h; // 0x0000000000000000
        public ulong Unknown_10h; // 0x0000000000000000
        public float Unknown_18h { get; set; } // 0, 0.1f, 1.0f
        public float Unknown_1Ch { get; set; } // 0, 0.8f, 1.0f, 1.1f, ...
        public uint Unknown_20h { get; set; } // eg. 0xff736626 - colour?
        public float Unknown_24h { get; set; } // 1.0f, 7.0f, 100.0f, ...
        public uint Unknown_28h { get; set; }  // 0, 4, 8, 9, 10, 11, 12, 14     //index/id
        public uint Unknown_2Ch; // 0x00000000
        public ulong Unknown_30h; // 0x0000000000000000
        public float Unknown_38h { get; set; } // 0, 0.1f, 0.3f, 1.0f
        public float Unknown_3Ch { get; set; } // 0, 1.0f, 1.1f, 1.2f, 1.4f, 1.5f
        public uint Unknown_40h { get; set; } // eg. 0xffffffff, 0xffffeca8  - colour?
        public float Unknown_44h { get; set; } // 0, 0.4f, 1.0f, 100.0f, ....
        public uint Unknown_48h { get; set; } // 0, 4, 8, 9, 10, 11, 12, 14     //index/id
        public uint Unknown_4Ch; // 0x00000000
        public ulong Unknown_50h; // 0x0000000000000000
        public ulong EffectRulePointer { get; set; }
        public ulong EffectRuleNamePointer { get; set; }
        public float Unknown_68h { get; set; } // 0, 0.5f, 1.0f
        public uint Unknown_6Ch { get; set; } // eg. 0x01010100

        // reference data
        public ParticleEffectRule EffectRule { get; set; }
        public string_r EffectRuleName { get; set; }

        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            // read structure data
            VFT = reader.ReadUInt32();
            Unknown_4h = reader.ReadUInt32();
            Unknown_8h = reader.ReadUInt64();
            Unknown_10h = reader.ReadUInt64();
            Unknown_18h = reader.ReadSingle();
            Unknown_1Ch = reader.ReadSingle();
            Unknown_20h = reader.ReadUInt32();
            Unknown_24h = reader.ReadSingle();
            Unknown_28h = reader.ReadUInt32();
            Unknown_2Ch = reader.ReadUInt32();
            Unknown_30h = reader.ReadUInt64();
            Unknown_38h = reader.ReadSingle();
            Unknown_3Ch = reader.ReadSingle();
            Unknown_40h = reader.ReadUInt32();
            Unknown_44h = reader.ReadSingle();
            Unknown_48h = reader.ReadUInt32();
            Unknown_4Ch = reader.ReadUInt32();
            Unknown_50h = reader.ReadUInt64();
            EffectRulePointer = reader.ReadUInt64();
            EffectRuleNamePointer = reader.ReadUInt64();
            Unknown_68h = reader.ReadSingle();
            Unknown_6Ch = reader.ReadUInt32();

            // read reference data
            EffectRule = reader.ReadBlockAt<ParticleEffectRule>(EffectRulePointer);
            EffectRuleName = reader.ReadBlockAt<string_r>(EffectRuleNamePointer);

            //if (EffectRuleName?.Value != (EffectRule?.Name?.Value ?? ""))
            //{ }//no hit

            //if (Unknown_4h != 1)
            //{ }
            //if (Unknown_8h != 0)
            //{ }//no hit
            //if (Unknown_10h != 0)
            //{ }//no hit
            //switch (Unknown_18h)
            //{
            //    case 0:
            //    case 1.0f:
            //    case 0.1f:
            //        break;
            //    default:
            //        break;//no hit
            //}
            switch (Unknown_1Ch)
            {
                case 0:
                case 1.0f:
                case 1.1f:
                case 0.8f:
                case 0.9f:
                case 1.5f:
                    break;
                default:
                    break;//more
            }
            //switch (Unknown_20h)
            //{
            //    case 0:
            //    case 0xffffffff:
            //    case 0x00ffffff:
            //    case 0xff736626:
            //    case 0xff404040:
            //    case 0xfffaf7c8:
            //    case 0xfffc42f9:
            //    case 0xff4f3535:
            //    case 0xff321a1a:
            //    case 0xffffd591:
            //        break;
            //    default:
            //        break;//no hit
            //}
            switch (Unknown_24h)
            {
                case 0:
                case 100.0f:
                case 0.6f:
                case 1.0f:
                case 0.3f:
                case 1.2f:
                case 7.0f:
                    break;
                default:
                    break;//more
            }
            //switch (Unknown_28h)
            //{
            //    case 0:
            //    case 8:
            //    case 11:
            //    case 9:
            //    case 12:
            //    case 10:
            //    case 14:
            //    case 4:
            //        break;
            //    default:
            //        break;//no hit
            //}
            //if (Unknown_2Ch != 0)
            //{ }//no hit
            //if (Unknown_30h != 0)
            //{ }//no hit
            //switch (Unknown_38h)
            //{
            //    case 0:
            //    case 1.0f:
            //    case 0.1f:
            //    case 0.3f:
            //        break;
            //    default:
            //        break;//no hit
            //}
            //switch (Unknown_3Ch)
            //{
            //    case 0:
            //    case 1.0f:
            //    case 1.1f:
            //    case 1.2f:
            //    case 1.4f:
            //    case 1.5f:
            //        break;
            //    default:
            //        break;//no hit
            //}
            //switch (Unknown_40h)
            //{
            //    case 0:
            //    case 0xffffffff:
            //    case 0xffffeca8:
            //    case 0xff8c7d2e:
            //    case 0xffd1d1d1:
            //    case 0xfff0dfb6:
            //    case 0xffcc16b4:
            //    case 0xff4c3434:
            //    case 0xff24341a:
            //    case 0xfffff1bd:
            //        break;
            //    default:
            //        break;//no hit
            //}
            switch (Unknown_44h)
            {
                case 0:
                case 100.0f:
                case 0.8f:
                case 1.0f:
                case 0.4f:
                case 1.8f:
                    break;
                default:
                    break;//more
            }
            //switch (Unknown_48h)
            //{
            //    case 0:
            //    case 8:
            //    case 11:
            //    case 9:
            //    case 12:
            //    case 10:
            //    case 14:
            //    case 4:
            //        break;
            //    default:
            //        break;//no hit
            //}
            //if (Unknown_4Ch != 0)
            //{ }//no hit
            //if (Unknown_50h != 0)
            //{ }//no hit
            //switch (Unknown_68h)
            //{
            //    case 0:
            //    case 1.0f:
            //    case 0.5f:
            //        break;
            //    default:
            //        break;//no hit
            //}
            //switch (Unknown_6Ch)
            //{
            //    case 0:
            //    case 1:
            //    case 0x00010000:
            //    case 0x00000100:
            //    case 0x00010101:
            //    case 0x01010100:
            //    case 0x00010100:
            //    case 0x01010101:
            //        break;
            //    default:
            //        break;//no hit
            //}

        }
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            // update structure data
            EffectRulePointer = (ulong)(EffectRule != null ? EffectRule.FilePosition : 0);
            EffectRuleNamePointer = (ulong)(EffectRuleName != null ? EffectRuleName.FilePosition : 0);

            // write structure data
            writer.Write(VFT);
            writer.Write(Unknown_4h);
            writer.Write(Unknown_8h);
            writer.Write(Unknown_10h);
            writer.Write(Unknown_18h);
            writer.Write(Unknown_1Ch);
            writer.Write(Unknown_20h);
            writer.Write(Unknown_24h);
            writer.Write(Unknown_28h);
            writer.Write(Unknown_2Ch);
            writer.Write(Unknown_30h);
            writer.Write(Unknown_38h);
            writer.Write(Unknown_3Ch);
            writer.Write(Unknown_40h);
            writer.Write(Unknown_44h);
            writer.Write(Unknown_48h);
            writer.Write(Unknown_4Ch);
            writer.Write(Unknown_50h);
            writer.Write(EffectRulePointer);
            writer.Write(EffectRuleNamePointer);
            writer.Write(Unknown_68h);
            writer.Write(Unknown_6Ch);
        }
        public void WriteXml(StringBuilder sb, int indent)
        {
            //YptXml.StringTag(sb, indent, "EffectRuleName", YptXml.XmlEscape(EffectRuleName?.Value ?? ""));
            YptXml.StringTag(sb, indent, "EffectRule", EffectRule?.Name?.Value ?? "");
            YptXml.ValueTag(sb, indent, "Unknown18", FloatUtil.ToString(Unknown_18h));
            YptXml.ValueTag(sb, indent, "Unknown1C", FloatUtil.ToString(Unknown_1Ch));
            YptXml.ValueTag(sb, indent, "Unknown20", YptXml.UintString(Unknown_20h));
            YptXml.ValueTag(sb, indent, "Unknown24", FloatUtil.ToString(Unknown_24h));
            YptXml.ValueTag(sb, indent, "Unknown28", Unknown_28h.ToString());
            YptXml.ValueTag(sb, indent, "Unknown38", FloatUtil.ToString(Unknown_38h));
            YptXml.ValueTag(sb, indent, "Unknown3C", FloatUtil.ToString(Unknown_3Ch));
            YptXml.ValueTag(sb, indent, "Unknown40", YptXml.UintString(Unknown_40h));
            YptXml.ValueTag(sb, indent, "Unknown44", FloatUtil.ToString(Unknown_44h));
            YptXml.ValueTag(sb, indent, "Unknown48", Unknown_48h.ToString());
            YptXml.ValueTag(sb, indent, "Unknown68", FloatUtil.ToString(Unknown_68h));
            YptXml.ValueTag(sb, indent, "Unknown6C", YptXml.UintString(Unknown_6Ch));
        }
        public void ReadXml(XmlNode node)
        {
            //EffectRuleName = (string_r)Xml.GetChildInnerText(node, "EffectRuleName"); if (EffectRuleName.Value == null) EffectRuleName = null;
            var ername = Xml.GetChildInnerText(node, "EffectRule");
            EffectRuleName = (string_r)(ername ?? "");
            Unknown_18h = Xml.GetChildFloatAttribute(node, "Unknown18");
            Unknown_1Ch = Xml.GetChildFloatAttribute(node, "Unknown1C");
            Unknown_20h = Xml.GetChildUIntAttribute(node, "Unknown20");
            Unknown_24h = Xml.GetChildFloatAttribute(node, "Unknown24");
            Unknown_28h = Xml.GetChildUIntAttribute(node, "Unknown28");
            Unknown_38h = Xml.GetChildFloatAttribute(node, "Unknown38");
            Unknown_3Ch = Xml.GetChildFloatAttribute(node, "Unknown3C");
            Unknown_40h = Xml.GetChildUIntAttribute(node, "Unknown40");
            Unknown_44h = Xml.GetChildFloatAttribute(node, "Unknown44");
            Unknown_48h = Xml.GetChildUIntAttribute(node, "Unknown48");
            Unknown_68h = Xml.GetChildFloatAttribute(node, "Unknown68");
            Unknown_6Ch = Xml.GetChildUIntAttribute(node, "Unknown6C");
            if (!string.IsNullOrEmpty(ername))
            { }
        }


        public override IResourceBlock[] GetReferences()
        {
            var list = new List<IResourceBlock>();
            if (EffectRule != null) list.Add(EffectRule);
            if (EffectRuleName != null) list.Add(EffectRuleName);
            return list.ToArray();
        }

        public override string ToString()
        {
            var str = EffectRuleName?.ToString();
            return (!string.IsNullOrEmpty(str)) ? str : base.ToString();
        }
    }


    [TC(typeof(EXP))] public class ParticleDrawable : ResourceSystemBlock, IMetaXmlItem
    {
        public override long BlockLength => 0x30;

        // structure data
        public float Unknown_0h { get; set; }
        public float Unknown_4h { get; set; }
        public float Unknown_8h { get; set; }
        public float Unknown_Ch { get; set; }
        public ulong NamePointer { get; set; }
        public ulong DrawablePointer { get; set; }
        public MetaHash NameHash { get; set; }
        public uint Unknown_24h { get; set; } // 0x00000000
        public ulong Unknown_28h; // 0x0000000000000000

        // reference data
        public string_r Name { get; set; }
        public DrawablePtfx Drawable { get; set; }

        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            // read structure data
            Unknown_0h = reader.ReadSingle();
            Unknown_4h = reader.ReadSingle();
            Unknown_8h = reader.ReadSingle();
            Unknown_Ch = reader.ReadSingle();
            NamePointer = reader.ReadUInt64();
            DrawablePointer = reader.ReadUInt64();
            NameHash = reader.ReadUInt32();
            Unknown_24h = reader.ReadUInt32();
            Unknown_28h = reader.ReadUInt64();

            // read reference data
            Name = reader.ReadBlockAt<string_r>(NamePointer);
            Drawable = reader.ReadBlockAt<DrawablePtfx>(DrawablePointer);

            if (!string.IsNullOrEmpty(Name?.Value))
            {
                JenkIndex.Ensure(Name.Value);
            }

            switch (Unknown_0h)
            {
                case 0.355044f:
                case 1.0f:
                case 0.308508f:
                    break;
                default:
                    break;//more
            }
            switch (Unknown_4h)
            {
                case 0.894308f:
                case 1.0f:
                case 0.127314f:
                    break;
                default:
                    break;//more
            }
            switch (Unknown_8h)
            {
                case 0.894308f:
                case 1.0f:
                case 0.127314f:
                    break;
                default:
                    break;//more
            }
            switch (Unknown_Ch)
            {
                case 0.4f:
                case 0.5f:
                case 0.178602f:
                    break;
                default:
                    break;//more
            }
            if (NameHash != JenkHash.GenHash(Name?.Value ?? ""))
            { }//no hit
            //if (Unknown_24h != 0)
            //{ }//no hit
            //if (Unknown_28h != 0)
            //{ }//no hit
        }
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            // update structure data
            NamePointer = (ulong)(Name != null ? Name.FilePosition : 0);
            DrawablePointer = (ulong)(Drawable != null ? Drawable.FilePosition : 0);

            // write structure data
            writer.Write(Unknown_0h);
            writer.Write(Unknown_4h);
            writer.Write(Unknown_8h);
            writer.Write(Unknown_Ch);
            writer.Write(NamePointer);
            writer.Write(DrawablePointer);
            writer.Write(NameHash);
            writer.Write(Unknown_24h);
            writer.Write(Unknown_28h);
        }
        public void WriteXml(StringBuilder sb, int indent)
        {
            YptXml.StringTag(sb, indent, "Name", YptXml.XmlEscape(Name?.Value ?? ""));
            YptXml.ValueTag(sb, indent, "Unknown0", FloatUtil.ToString(Unknown_0h));
            YptXml.ValueTag(sb, indent, "Unknown4", FloatUtil.ToString(Unknown_4h));
            YptXml.ValueTag(sb, indent, "Unknown8", FloatUtil.ToString(Unknown_8h));
            YptXml.ValueTag(sb, indent, "UnknownC", FloatUtil.ToString(Unknown_Ch));
            if (Drawable != null)
            {
            }
        }
        public void ReadXml(XmlNode node)
        {
            Name = (string_r)Xml.GetChildInnerText(node, "Name"); if (Name.Value == null) Name = null;
            NameHash = JenkHash.GenHash(Name?.Value ?? "");
            Unknown_0h = Xml.GetChildFloatAttribute(node, "Unknown0");
            Unknown_4h = Xml.GetChildFloatAttribute(node, "Unknown4");
            Unknown_8h = Xml.GetChildFloatAttribute(node, "Unknown8");
            Unknown_Ch = Xml.GetChildFloatAttribute(node, "UnknownC");
        }

        public override IResourceBlock[] GetReferences()
        {
            var list = new List<IResourceBlock>();
            if (Name != null) list.Add(Name);
            if (Drawable != null) list.Add(Drawable);
            return list.ToArray();
        }

        public override string ToString()
        {
            if (!string.IsNullOrEmpty(Name?.Value)) return Name.Value;
            if (NameHash != 0) return NameHash.ToString();
            return base.ToString();
        }

    }










    [TC(typeof(EXP))] public class ParticleEffectRule : ResourceSystemBlock
    {
        public override long BlockLength => 0x3C0;

        // structure data
        public uint VFT { get; set; }
        public uint Unknown_4h = 1; // 0x00000001
        public ulong Unknown_8h; // 0x0000000000000000
        public ulong Unknown_10h = 1; // 0x0000000000000001
        public float Unknown_18h { get; set; } = 4.2f;
        public uint Unknown_1Ch; // 0x00000000
        public ulong NamePointer { get; set; }
        public ulong Unknown_28h { get; set; } = 0x0000000050000000; // 0x50000000 -> ".?AVptxFxList@rage@@" pointer to itself
        public uint VFT2 { get; set; } = 0x4060e3e8; // 0x4060e3e8, 0x40610408
        public uint Unknown_34h = 1; // 0x00000001
        public ulong EventEmittersPointer { get; set; }
        public ushort EventEmittersCount { get; set; }
        public ushort EventEmittersCapacity { get; set; } = 32; //always 32
        public uint Unknown_44h; // 0x00000000
        public ulong UnknownData1Pointer { get; set; }
        public int NumLoops { get; set; } // 0, 0xffffffff
        public byte SortEventsByDistance { get; set; }
        public byte DrawListID { get; set; }
        public byte IsShortLived { get; set; }
        public byte HasNoShadows { get; set; }
        public ulong padding00;
        public Vector3 VRandomOffsetPos { get; set; }
        public uint padding01 { get; set; } = 0x7f800001;
        public float PreUpdateTime { get; set; }
        public float PreUpdateTimeInterval { get; set; } // 0, 0.1f, 0.25f, 1.0f
        public float DurationMin { get; set; }
        public float DurationMax { get; set; }
        public float PlaybackRateScalarMin { get; set; }
        public float PlaybackRateScalarMax { get; set; }
        public byte ViewportCullingMode { get; set; }
        public byte RenderWhenViewportCulled { get; set; }
        public byte UpdateWhenViewportCulled { get; set; }
        public byte EmitWhenViewportCulled { get; set; }
        public byte DistanceCullingMode { get; set; }
        public byte RenderWhenDistanceCulled { get; set; }
        public byte UpdateWhenDistanceCulled { get; set; }
        public byte EmitWhenDistanceCulled { get; set; }
        public Vector3 ViewportCullingSphereOffset { get; set; }
        public uint padding02 { get; set; } = 0x7f800001;
        public float ViewportCullingSphereRadius { get; set; }
        public float DistanceCullingFadeDist { get; set; }
        public float DistanceCullingCullDist { get; set; }
        public float LodEvoDistanceMin { get; set; }
        public float LodEvoDistanceMax { get; set; }
        public float CollisionRange { get; set; }
        public float CollisionProbeDistance { get; set; }
        public byte CollisionType { get; set; }
        public byte ShareEntityCollisions { get; set; }
        public byte OnlyUseBVHCollisions { get; set; }
        public byte GameFlags { get; set; }
        public ParticleKeyframeProp KeyframeProp0 { get; set; }
        public ParticleKeyframeProp KeyframeProp1 { get; set; }
        public ParticleKeyframeProp KeyframeProp2 { get; set; }
        public ParticleKeyframeProp KeyframeProp3 { get; set; }
        public ParticleKeyframeProp KeyframeProp4 { get; set; }
        public ulong KeyframePropsPointer { get; set; } //pointer to a list, which is pointing back to above items
        public ushort KeyframePropsCount { get; set; } = 5; //always 5
        public ushort KeyframePropsCapacity { get; set; } = 16; //always 16
        public uint Unknown_39Ch; // 0x00000000
        public byte ColourTintMaxEnable { get; set; }
        public byte UseDataVolume { get; set; }
        public byte DataVolumeType { get; set; }
        public byte padding03 { get; set; }
        public uint Unknown_3A4h; // 0x00000000
        public float ZoomLevel { get; set; }
        public uint Unknown_3ACh { get; set; } // 0x00000000
        public ulong Unknown_3B0h { get; set; } // 0x0000000000000000
        public ulong Unknown_3B8h { get; set; } // 0x0000000000000000

        // reference data
        public string_r Name { get; set; }
        public MetaHash NameHash { get; set; }
        public ResourcePointerArray64<ParticleEventEmitter> EventEmitters { get; set; }
        public ParticleUnknown1 UnknownData { get; set; }
        public ResourcePointerArray64<ParticleKeyframeProp> KeyframeProps { get; set; } // these just point to the 5x embedded KeyframeProps, padded to 16 items


        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            #region read

            // read structure data
            VFT = reader.ReadUInt32();
            Unknown_4h = reader.ReadUInt32();
            Unknown_8h = reader.ReadUInt64();
            Unknown_10h = reader.ReadUInt64();
            Unknown_18h = reader.ReadSingle();
            Unknown_1Ch = reader.ReadUInt32();
            NamePointer = reader.ReadUInt64();
            Unknown_28h = reader.ReadUInt64();
            VFT2 = reader.ReadUInt32();
            Unknown_34h = reader.ReadUInt32();
            EventEmittersPointer = reader.ReadUInt64();
            EventEmittersCount = reader.ReadUInt16();
            EventEmittersCapacity = reader.ReadUInt16();
            Unknown_44h = reader.ReadUInt32();
            UnknownData1Pointer = reader.ReadUInt64();
            NumLoops = reader.ReadInt32();
            SortEventsByDistance = reader.ReadByte();
            DrawListID = reader.ReadByte();
            IsShortLived = reader.ReadByte();
            HasNoShadows = reader.ReadByte();
            padding00 = reader.ReadUInt64();
            VRandomOffsetPos = new Vector3(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
            padding01 = reader.ReadUInt32();
            PreUpdateTime = reader.ReadSingle();
            PreUpdateTimeInterval = reader.ReadSingle();
            DurationMin = reader.ReadSingle();
            DurationMax = reader.ReadSingle();
            PlaybackRateScalarMin = reader.ReadSingle();
            PlaybackRateScalarMax = reader.ReadSingle();
            ViewportCullingMode = reader.ReadByte();
            RenderWhenViewportCulled = reader.ReadByte();
            UpdateWhenViewportCulled = reader.ReadByte();
            EmitWhenViewportCulled = reader.ReadByte();
            DistanceCullingMode = reader.ReadByte();
            RenderWhenDistanceCulled = reader.ReadByte();
            UpdateWhenDistanceCulled = reader.ReadByte();
            EmitWhenDistanceCulled = reader.ReadByte();
            ViewportCullingSphereOffset = new Vector3(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
            padding02 = reader.ReadUInt32();
            ViewportCullingSphereRadius = reader.ReadSingle();
            DistanceCullingFadeDist = reader.ReadSingle();
            DistanceCullingCullDist = reader.ReadSingle();
            LodEvoDistanceMin = reader.ReadSingle();
            LodEvoDistanceMax = reader.ReadSingle();
            CollisionRange = reader.ReadSingle();
            CollisionProbeDistance = reader.ReadSingle();
            CollisionType = reader.ReadByte();
            ShareEntityCollisions = reader.ReadByte();
            OnlyUseBVHCollisions = reader.ReadByte();
            GameFlags = reader.ReadByte();
            KeyframeProp0 = reader.ReadBlock<ParticleKeyframeProp>();
            KeyframeProp1 = reader.ReadBlock<ParticleKeyframeProp>();
            KeyframeProp2 = reader.ReadBlock<ParticleKeyframeProp>();
            KeyframeProp3 = reader.ReadBlock<ParticleKeyframeProp>();
            KeyframeProp4 = reader.ReadBlock<ParticleKeyframeProp>();
            KeyframePropsPointer = reader.ReadUInt64();
            KeyframePropsCount = reader.ReadUInt16();
            KeyframePropsCapacity = reader.ReadUInt16();
            Unknown_39Ch = reader.ReadUInt32();
            ColourTintMaxEnable = reader.ReadByte();
            UseDataVolume = reader.ReadByte();
            DataVolumeType = reader.ReadByte();
            padding03 = reader.ReadByte();
            Unknown_3A4h = reader.ReadUInt32();
            ZoomLevel = reader.ReadSingle();
            Unknown_3ACh = reader.ReadUInt32();
            Unknown_3B0h = reader.ReadUInt64();
            Unknown_3B8h = reader.ReadUInt64();

            // read reference data
            Name = reader.ReadBlockAt<string_r>(NamePointer);
            EventEmitters = reader.ReadBlockAt<ResourcePointerArray64<ParticleEventEmitter>>(EventEmittersPointer, EventEmittersCapacity);
            UnknownData = reader.ReadBlockAt<ParticleUnknown1>(UnknownData1Pointer);
            KeyframeProps = reader.ReadBlockAt<ResourcePointerArray64<ParticleKeyframeProp>>(KeyframePropsPointer, KeyframePropsCapacity);

            if (!string.IsNullOrEmpty(Name?.Value))
            {
                JenkIndex.Ensure(Name.Value);
                NameHash = JenkHash.GenHash(Name.Value);
            }

            #endregion


            #region testing

            //for (int i = 0; i < (EventEmitters?.data_items?.Length??0); i++)
            //{
            //    if (EventEmitters.data_items[i].Index != i)
            //    { }//no hit
            //}

            //if (EventEmittersCount2 != 32)
            //{ }//no hit
            //if (KeyframePropsCount2 != 16)
            //{ }//no hit
            //if (KeyframePropsCount1 != 5)
            //{ }//no hit

            //if (Unknown_4h != 1)
            //{ }//no hit
            //if (Unknown_8h != 0)
            //{ }//no hit
            //if (Unknown_10h != 1)
            //{ }//no hit
            //if (Unknown_18h != 4.2f)
            //{ }//no hit
            //if (Unknown_1Ch != 0)
            //{ }//no hit
            //switch (Unknown_28h)
            //{
            //    case 0x0000000050000000:
            //        break;
            //    default:
            //        break;//no hit
            //}
            //switch (VFT2) //some VFT
            //{
            //    case 0x4060e3e8: 
            //    case 0x40610408:
            //        break;
            //    default:
            //        break;//no hit
            //}
            //if (Unknown_34h != 1)
            //{ }//no hit
            //if (Unknown_44h != 0)
            //{ }//no hit
            //switch (Unknown_50h)
            //{
            //    case 0xffffffff:
            //    case 0:
            //        break;
            //    default:
            //        break;//no hit
            //}
            //switch (Unknown_54h)
            //{
            //    case 0x01000000:
            //    case 0x01010001:
            //    case 0x01010200:
            //    case 0x01010000:
            //    case 0x01000200:
            //    case 0x01000001:
            //    case 0x01000201:
            //    case 0x01000100:
            //        break;
            //    default:
            //        break;//more
            //}
            //if (Unknown_58h != 0)
            //{ }//no hit
            //if (Unknown_60h != 0)
            //{ }//no hit
            //if ((Unknown_68h != 0) && (Unknown_68h != 0x80000000))//float?
            //{ }//no hit
            //if (Unknown_6Ch != 0x7f800001)
            //{ }//no hit
            //switch (Unknown_70h)
            //{
            //    case 0:
            //    case 1.0f:
            //    case 0.5f:
            //    case 0.2f:
            //    case 0.1f:
            //        break;
            //    default:
            //        break;//more
            //}
            //switch (Unknown_74h)
            //{
            //    case 0.25f:
            //    case 0:
            //    case 1.0f:
            //    case 0.1f:
            //        break;
            //    default:
            //        break;//no hit
            //}
            //switch (Unknown_78h)
            //{
            //    case 0.2f:
            //    case 0.5f:
            //    case 1.0f:
            //        break;
            //    default:
            //        break;//more
            //}
            //switch (Unknown_7Ch)
            //{
            //    case 0.2f:
            //    case 0.5f:
            //    case 1.0f:
            //        break;
            //    default:
            //        break;//more
            //}
            //switch (Unknown_80h)
            //{
            //    case 1.0f:
            //    case 2.0f:
            //    case 1.2f:
            //    case 1.5f:
            //        break;
            //    default:
            //        break;//more
            //}
            //switch (Unknown_84h)
            //{
            //    case 1.0f:
            //    case 2.0f:
            //        break;
            //    default:
            //        break;//more
            //}
            //switch (Unknown_88h)
            //{
            //    case 0x01010100:
            //    case 0x01010101:
            //    case 0x00010004:
            //    case 0x01010002:
            //    case 0x00000003:
            //    case 0x01010105:
            //    case 0x00010105:
            //        break;
            //    default:
            //        break;//no hit
            //}
            //switch (Unknown_8Ch)
            //{
            //    case 0x00010004:
            //    case 0x01010101:
            //    case 0x01010100:
            //    case 0x01010002:
            //    case 0x00000003:
            //    case 0x00010105:
            //    case 0x00000005:
            //        break;
            //    default:
            //        break;//no hit
            //}
            //switch (Unknown_90h)
            //{
            //    case 0:
            //    case 1.1f:
            //    case 1.5f:
            //    case 1.2f:
            //    case 6.0f:
            //        break;
            //    default:
            //        break;//more
            //}
            //switch (Unknown_94h)
            //{
            //    case 0:
            //    case 1.8f:
            //    case 10.0f:
            //    case 0.4f:
            //    case -1.0f:
            //    case -9.0f:
            //        break;
            //    default:
            //        break;//more
            //}
            //switch (Unknown_98h)
            //{
            //    case 0:
            //    case 5.0f:
            //    case 1.5f:
            //    case -1.0f:
            //    case 0.5f:
            //    case 0.2f:
            //    case 1.0f:
            //    case 12.0f:
            //        break;
            //    default:
            //        break;//more
            //}
            //if (Unknown_9Ch != 0x7f800001)
            //{ }//no hit
            //switch (Unknown_A0h)
            //{
            //    case 0:
            //    case 4.5f:
            //    case 11.0f:
            //    case 5.0f:
            //        break;
            //    default:
            //        break;//and more
            //}
            //switch (Unknown_A4h)
            //{
            //    case 38.0f:
            //    case 25.0f:
            //        break;
            //    default:
            //        break;//and more
            //}
            //switch (Unknown_A8h)
            //{
            //    case 40.0f:
            //    case 30.0f:
            //        break;
            //    default:
            //        break;//and more
            //}
            //switch (Unknown_ACh)
            //{
            //    case 15.0f:
            //    case 4.0f:
            //        break;
            //    default:
            //        break;//and more
            //}
            //switch (Unknown_B0h)
            //{
            //    case 40.0f:
            //    case 12.0f:
            //        break;
            //    default:
            //        break;//and more
            //}
            //switch (Unknown_B4h)
            //{
            //    case 3.0f:
            //    case 0:
            //    case 0.500002f:
            //    case 1.5f:
            //        break;
            //    default:
            //        break;//more
            //}
            //switch (Unknown_B8h)
            //{
            //    case 2.0f:
            //    case 0:
            //    case 1.5f:
            //    case 1.0f:
            //    case 3.0f:
            //    case 5.0f:
            //    case 9.0f:
            //        break;
            //    default:
            //        break;//more
            //}
            //switch (Unknown_BCh)
            //{
            //    case 0x00010103:
            //    case 0:
            //    case 0x01000000:
            //    case 0x01010003:
            //    case 0x00000103:
            //    case 0x00000002:
            //    case 0x00000003:
            //    case 0x00010100:
            //    case 0x01000002:
            //    case 0x00010002:
            //    case 0x01010002:
            //        break;
            //    default:
            //        break;//more
            //}
            //if (Unknown_39Ch != 0)
            //{ }//no hit
            //switch (Unknown_3A0h)
            //{
            //    case 0:
            //    case 1:
            //    case 0x00000100:
            //    case 0x00010100:
            //    case 0x00020100:
            //    case 0x00080000:
            //    case 0x00090100:
            //    case 0x000b0100:
            //    case 0x000c0100: //setting the 5th digit to C (eg 0x000C0000) for Unknown3A0 in EffectRuleDictionary enables damage for volumetric particles -Monika
            //        break;
            //    default:
            //        break;//no hit
            //}
            //if (Unknown_3A4h != 0)
            //{ }//no hit
            //if (Unknown_3A8h != 100.0f)
            //{ }//no hit
            //if (Unknown_3ACh != 0)
            //{ }//no hit
            //if (Unknown_3B0h != 0)
            //{ }//no hit
            //if (Unknown_3B8h != 0)
            //{ }//no hit

            #endregion

        }
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            // update structure data
            NamePointer = (ulong)(Name != null ? Name.FilePosition : 0);
            EventEmittersPointer = (ulong)(EventEmitters != null ? EventEmitters.FilePosition : 0);
            UnknownData1Pointer = (ulong)(UnknownData != null ? UnknownData.FilePosition : 0);
            KeyframePropsPointer = (ulong)(KeyframeProps != null ? KeyframeProps.FilePosition : 0);

            // write structure data
            writer.Write(VFT);
            writer.Write(Unknown_4h);
            writer.Write(Unknown_8h);
            writer.Write(Unknown_10h);
            writer.Write(Unknown_18h);
            writer.Write(Unknown_1Ch);
            writer.Write(NamePointer);
            writer.Write(Unknown_28h);
            writer.Write(VFT2);
            writer.Write(Unknown_34h);
            writer.Write(EventEmittersPointer);
            writer.Write(EventEmittersCount);
            writer.Write(EventEmittersCapacity);
            writer.Write(Unknown_44h);
            writer.Write(UnknownData1Pointer);
            writer.Write(NumLoops);
            writer.Write(SortEventsByDistance);
            writer.Write(DrawListID);
            writer.Write(IsShortLived);
            writer.Write(HasNoShadows);
            writer.Write(padding00);
            writer.Write(VRandomOffsetPos);
            writer.Write(padding01);
            writer.Write(PreUpdateTime);
            writer.Write(PreUpdateTimeInterval);
            writer.Write(DurationMin);
            writer.Write(DurationMax);
            writer.Write(PlaybackRateScalarMin);
            writer.Write(PlaybackRateScalarMax);
            writer.Write(ViewportCullingMode);
            writer.Write(RenderWhenViewportCulled);
            writer.Write(UpdateWhenViewportCulled);
            writer.Write(EmitWhenViewportCulled);
            writer.Write(DistanceCullingMode);
            writer.Write(RenderWhenDistanceCulled);
            writer.Write(UpdateWhenDistanceCulled);
            writer.Write(EmitWhenDistanceCulled);
            writer.Write(ViewportCullingSphereOffset);
            writer.Write(padding02);
            writer.Write(ViewportCullingSphereRadius);
            writer.Write(DistanceCullingFadeDist);
            writer.Write(DistanceCullingCullDist);
            writer.Write(LodEvoDistanceMin);
            writer.Write(LodEvoDistanceMax);
            writer.Write(CollisionRange);
            writer.Write(CollisionProbeDistance);
            writer.Write(CollisionType);
            writer.Write(ShareEntityCollisions);
            writer.Write(OnlyUseBVHCollisions);
            writer.Write(GameFlags);
            writer.WriteBlock(KeyframeProp0);
            writer.WriteBlock(KeyframeProp1);
            writer.WriteBlock(KeyframeProp2);
            writer.WriteBlock(KeyframeProp3);
            writer.WriteBlock(KeyframeProp4);
            writer.Write(KeyframePropsPointer);
            writer.Write(KeyframePropsCount);
            writer.Write(KeyframePropsCapacity);
            writer.Write(Unknown_39Ch);
            writer.Write(ColourTintMaxEnable);
            writer.Write(UseDataVolume);
            writer.Write(DataVolumeType);
            writer.Write(padding03);
            writer.Write(Unknown_3A4h);
            writer.Write(ZoomLevel);
            writer.Write(Unknown_3ACh);
            writer.Write(Unknown_3B0h);
            writer.Write(Unknown_3B8h);
        }
        public void WriteXml(StringBuilder sb, int indent)
        {
            YptXml.StringTag(sb, indent, "Name", YptXml.XmlEscape(Name?.Value ?? ""));
            YptXml.ValueTag(sb, indent, "NumLoops", YptXml.UintString((uint)NumLoops));
            YptXml.ValueTag(sb, indent, "SortEventsByDistance", FloatUtil.ToString(SortEventsByDistance));
            YptXml.ValueTag(sb, indent, "DrawListID", FloatUtil.ToString(DrawListID));
            YptXml.ValueTag(sb, indent, "IsShortLived", FloatUtil.ToString(IsShortLived));
            YptXml.ValueTag(sb, indent, "HasNoShadows", FloatUtil.ToString(HasNoShadows));
            RelXml.SelfClosingTag(sb, indent, "VRandomOffsetPos " + FloatUtil.GetVector3XmlString(VRandomOffsetPos));
            YptXml.ValueTag(sb, indent, "PreUpdateTime", FloatUtil.ToString(PreUpdateTime));
            YptXml.ValueTag(sb, indent, "PreUpdateTimeInterval", FloatUtil.ToString(PreUpdateTimeInterval));
            YptXml.ValueTag(sb, indent, "DurationMin", FloatUtil.ToString(DurationMin));
            YptXml.ValueTag(sb, indent, "DurationMax", FloatUtil.ToString(DurationMax));
            YptXml.ValueTag(sb, indent, "PlaybackRateScalarMin", FloatUtil.ToString(PlaybackRateScalarMin));
            YptXml.ValueTag(sb, indent, "PlaybackRateScalarMax", FloatUtil.ToString(PlaybackRateScalarMax));
            YptXml.ValueTag(sb, indent, "ViewportCullingMode", FloatUtil.ToString(ViewportCullingMode));
            YptXml.ValueTag(sb, indent, "RenderWhenViewportCulled", FloatUtil.ToString(RenderWhenViewportCulled));
            YptXml.ValueTag(sb, indent, "UpdateWhenViewportCulled", FloatUtil.ToString(UpdateWhenViewportCulled));
            YptXml.ValueTag(sb, indent, "EmitWhenViewportCulled", FloatUtil.ToString(EmitWhenViewportCulled));
            YptXml.ValueTag(sb, indent, "DistanceCullingMode", FloatUtil.ToString(DistanceCullingMode));
            YptXml.ValueTag(sb, indent, "RenderWhenDistanceCulled", FloatUtil.ToString(RenderWhenDistanceCulled));
            YptXml.ValueTag(sb, indent, "UpdateWhenDistanceCulled", FloatUtil.ToString(UpdateWhenDistanceCulled));
            YptXml.ValueTag(sb, indent, "EmitWhenDistanceCulled", FloatUtil.ToString(EmitWhenDistanceCulled));
            RelXml.SelfClosingTag(sb, indent, "ViewportCullingSphereOffset " + FloatUtil.GetVector3XmlString(ViewportCullingSphereOffset));
            YptXml.ValueTag(sb, indent, "ViewportCullingSphereRadius", FloatUtil.ToString(ViewportCullingSphereRadius));
            YptXml.ValueTag(sb, indent, "DistanceCullingFadeDist", FloatUtil.ToString(DistanceCullingFadeDist));
            YptXml.ValueTag(sb, indent, "DistanceCullingCullDist", FloatUtil.ToString(DistanceCullingCullDist));
            YptXml.ValueTag(sb, indent, "LodEvoDistanceMin", FloatUtil.ToString(LodEvoDistanceMin));
            YptXml.ValueTag(sb, indent, "LodEvoDistanceMax", FloatUtil.ToString(LodEvoDistanceMax));
            YptXml.ValueTag(sb, indent, "CollisionRange", FloatUtil.ToString(CollisionRange));
            YptXml.ValueTag(sb, indent, "CollisionProbeDistance", FloatUtil.ToString(CollisionProbeDistance));
            YptXml.ValueTag(sb, indent, "CollisionType", FloatUtil.ToString(CollisionType));
            YptXml.ValueTag(sb, indent, "ShareEntityCollisions", FloatUtil.ToString(ShareEntityCollisions));
            YptXml.ValueTag(sb, indent, "OnlyUseBVHCollisions", FloatUtil.ToString(OnlyUseBVHCollisions));
            YptXml.ValueTag(sb, indent, "GameFlags", FloatUtil.ToString(GameFlags));
            YptXml.ValueTag(sb, indent, "ColourTintMaxEnable", FloatUtil.ToString(ColourTintMaxEnable));
            YptXml.ValueTag(sb, indent, "UseDataVolume", FloatUtil.ToString(UseDataVolume));
            YptXml.ValueTag(sb, indent, "DataVolumeType", FloatUtil.ToString(DataVolumeType));
            YptXml.ValueTag(sb, indent, "ZoomLevel", FloatUtil.ToString(ZoomLevel));
            if (EventEmitters?.data_items != null)
            {
                var ee = new ParticleEventEmitter[EventEmittersCount];//trim the unused items from this array
                Array.Copy(EventEmitters.data_items, 0, ee, 0, EventEmittersCount);
                YptXml.WriteItemArray(sb, ee, indent, "EventEmitters");
            }
            if (KeyframeProps?.data_items != null)
            {
                var kp = new ParticleKeyframeProp[KeyframePropsCount];//trim the unused items from this array
                Array.Copy(KeyframeProps.data_items, 0, kp, 0, KeyframePropsCount);
                YptXml.WriteItemArray(sb, kp, indent, "KeyframeProperties");
            }
            if (UnknownData != null)
            {
                YptXml.OpenTag(sb, indent, "UnknownData");
                UnknownData.WriteXml(sb, indent + 1);
                YptXml.CloseTag(sb, indent, "UnknownData");
            }
        }
        public void ReadXml(XmlNode node)
        {
            Name = (string_r)Xml.GetChildInnerText(node, "Name"); if (Name.Value == null) Name = null;
            NameHash = JenkHash.GenHash(Name?.Value ?? "");
            NumLoops = (int)Xml.GetChildUIntAttribute(node, "NumLoops");
            SortEventsByDistance = (byte)Xml.GetChildFloatAttribute(node, "SortEventsByDistance");
            DrawListID = (byte)Xml.GetChildFloatAttribute(node, "DrawListID");
            IsShortLived = (byte)Xml.GetChildFloatAttribute(node, "IsShortLived");
            HasNoShadows = (byte)Xml.GetChildFloatAttribute(node, "HasNoShadows");
            VRandomOffsetPos = Xml.GetChildVector3Attributes(node, "VRandomOffsetPos");
            PreUpdateTime = Xml.GetChildFloatAttribute(node, "PreUpdateTime");
            PreUpdateTimeInterval = Xml.GetChildFloatAttribute(node, "PreUpdateTimeInterval");
            DurationMin = Xml.GetChildFloatAttribute(node, "DurationMin");
            DurationMax = Xml.GetChildFloatAttribute(node, "DurationMax");
            PlaybackRateScalarMin = Xml.GetChildFloatAttribute(node, "PlaybackRateScalarMin");
            PlaybackRateScalarMax = Xml.GetChildFloatAttribute(node, "PlaybackRateScalarMax");
            ViewportCullingMode = (byte)Xml.GetChildFloatAttribute(node, "ViewportCullingMode");
            RenderWhenViewportCulled = (byte)Xml.GetChildFloatAttribute(node, "RenderWhenViewportCulled");
            UpdateWhenViewportCulled = (byte)Xml.GetChildFloatAttribute(node, "UpdateWhenViewportCulled");
            EmitWhenViewportCulled = (byte)Xml.GetChildFloatAttribute(node, "EmitWhenViewportCulled");
            DistanceCullingMode = (byte)Xml.GetChildFloatAttribute(node, "DistanceCullingMode");
            RenderWhenDistanceCulled = (byte)Xml.GetChildFloatAttribute(node, "RenderWhenDistanceCulled");
            UpdateWhenDistanceCulled = (byte)Xml.GetChildFloatAttribute(node, "UpdateWhenDistanceCulled");
            EmitWhenDistanceCulled = (byte)Xml.GetChildFloatAttribute(node, "EmitWhenDistanceCulled");
            ViewportCullingSphereOffset = Xml.GetChildVector3Attributes(node, "ViewportCullingSphereOffset");
            ViewportCullingSphereRadius = Xml.GetChildFloatAttribute(node, "ViewportCullingSphereRadius");
            DistanceCullingFadeDist = Xml.GetChildFloatAttribute(node, "DistanceCullingFadeDist");
            DistanceCullingCullDist = Xml.GetChildFloatAttribute(node, "DistanceCullingCullDist");
            LodEvoDistanceMin = Xml.GetChildFloatAttribute(node, "LodEvoDistanceMin");
            LodEvoDistanceMax = Xml.GetChildFloatAttribute(node, "LodEvoDistanceMax");
            CollisionRange = Xml.GetChildFloatAttribute(node, "CollisionRange");
            CollisionProbeDistance = Xml.GetChildFloatAttribute(node, "CollisionProbeDistance");
            CollisionType = (byte)Xml.GetChildFloatAttribute(node, "CollisionType");
            ShareEntityCollisions = (byte)Xml.GetChildFloatAttribute(node, "ShareEntityCollisions");
            OnlyUseBVHCollisions = (byte)Xml.GetChildFloatAttribute(node, "OnlyUseBVHCollisions");
            GameFlags = (byte)Xml.GetChildFloatAttribute(node, "GameFlags");
            ColourTintMaxEnable = (byte)Xml.GetChildFloatAttribute(node, "ColourTintMaxEnable");
            UseDataVolume = (byte)Xml.GetChildFloatAttribute(node, "UseDataVolume");
            DataVolumeType = (byte)Xml.GetChildFloatAttribute(node, "DataVolumeType");
            ZoomLevel = Xml.GetChildFloatAttribute(node, "ZoomLevel");

            var emlist = XmlMeta.ReadItemArray<ParticleEventEmitter>(node, "EventEmitters")?.ToList() ?? new List<ParticleEventEmitter>();
            EventEmittersCount = (ushort)emlist.Count;
            for (int i = emlist.Count; i < 32; i++) emlist.Add(null);
            EventEmitters = new ResourcePointerArray64<ParticleEventEmitter>();
            EventEmitters.data_items = emlist.ToArray();
            for (int i = 0; i < (EventEmitters.data_items?.Length ?? 0); i++)
            {
                if (EventEmitters.data_items[i] != null)
                {
                    EventEmitters.data_items[i].Index = (uint)i;
                }
            }


            var kflist = XmlMeta.ReadItemArray<ParticleKeyframeProp>(node, "KeyframeProperties")?.ToList() ?? new List<ParticleKeyframeProp>();
            KeyframeProp0 = (kflist.Count > 0) ? kflist[0] : new ParticleKeyframeProp();
            KeyframeProp1 = (kflist.Count > 1) ? kflist[1] : new ParticleKeyframeProp();
            KeyframeProp2 = (kflist.Count > 2) ? kflist[2] : new ParticleKeyframeProp();
            KeyframeProp3 = (kflist.Count > 3) ? kflist[3] : new ParticleKeyframeProp();
            KeyframeProp4 = (kflist.Count > 4) ? kflist[4] : new ParticleKeyframeProp();
            for (int i = kflist.Count; i < 16; i++) kflist.Add(null);
            KeyframeProps = new ResourcePointerArray64<ParticleKeyframeProp>();
            KeyframeProps.data_items = kflist.ToArray();
            KeyframeProps.ManualReferenceOverride = true;
            KeyframePropsCount = 5;//this should always be 5.......
            KeyframePropsCapacity = 16;//should always be 16...

            var udnode = node.SelectSingleNode("UnknownData");
            if (udnode != null)
            {
                UnknownData = new ParticleUnknown1();
                UnknownData.ReadXml(udnode);
            }

        }

        public override IResourceBlock[] GetReferences()
        {
            var list = new List<IResourceBlock>();
            if (Name != null) list.Add(Name);
            if (EventEmitters != null) list.Add(EventEmitters);
            if (UnknownData != null) list.Add(UnknownData);
            if (KeyframeProps != null)
            {
                KeyframeProps.ManualReferenceOverride = true;
                list.Add(KeyframeProps);
            }
            return list.ToArray();
        }

        public override Tuple<long, IResourceBlock>[] GetParts()
        {
            return new Tuple<long, IResourceBlock>[] {
                new Tuple<long, IResourceBlock>(192, KeyframeProp0),
                new Tuple<long, IResourceBlock>(336, KeyframeProp1),
                new Tuple<long, IResourceBlock>(480, KeyframeProp2),
                new Tuple<long, IResourceBlock>(624, KeyframeProp3),
                new Tuple<long, IResourceBlock>(768, KeyframeProp4)
            };
        }

        public override string ToString()
        {
            return Name?.ToString() ?? base.ToString();
        }
    }


    [TC(typeof(EXP))] public class ParticleEventEmitter : ResourceSystemBlock, IMetaXmlItem
    {
        // ptxEvent
        // ptxEventEmitter
        public override long BlockLength => 0x70;

        // structure data
        public uint VFT { get; set; }
        public uint Unknown_4h = 1; // 0x00000001
        public uint Index { get; set; } // 0, 1, 2, 3, 4, 5, 6  -index?
        public uint Unknown_Ch; // 0x00000000
        public float Unknown_10h { get; set; }
        public float Unknown_14h { get; set; }
        public ulong UnknownDataPointer { get; set; }
        public ulong Unknown_20h; // 0x0000000000000000
        public ulong Unknown_28h; // 0x0000000000000000
        public ulong EmitterRuleNamePointer { get; set; }
        public ulong ParticleRuleNamePointer { get; set; }
        public ulong EmitterRulePointer { get; set; }
        public ulong ParticleRulePointer { get; set; }
        public float PlaybackRateScalarMin { get; set; }
        public float PlaybackRateScalarMax { get; set; }
        public float ZoomScalarMin { get; set; }
        public float ZoomScalarMax { get; set; }
        public uint ColourTintMin { get; set; }
        public uint ColourTintMax { get; set; }
        public ulong Unknown_68h; // 0x0000000000000000

        // reference data
        public ParticleUnknown1 UnknownData { get; set; }
        public string_r EmitterRuleName { get; set; }
        public string_r ParticleRuleName { get; set; }
        public ParticleEmitterRule EmitterRule { get; set; }
        public ParticleRule ParticleRule { get; set; }


        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            // read structure data
            VFT = reader.ReadUInt32();
            Unknown_4h = reader.ReadUInt32();
            Index = reader.ReadUInt32();
            Unknown_Ch = reader.ReadUInt32();
            Unknown_10h = reader.ReadSingle();
            Unknown_14h = reader.ReadSingle();
            UnknownDataPointer = reader.ReadUInt64();
            Unknown_20h = reader.ReadUInt64();
            Unknown_28h = reader.ReadUInt64();
            EmitterRuleNamePointer = reader.ReadUInt64();
            ParticleRuleNamePointer = reader.ReadUInt64();
            EmitterRulePointer = reader.ReadUInt64();
            ParticleRulePointer = reader.ReadUInt64();
            PlaybackRateScalarMin = reader.ReadSingle();
            PlaybackRateScalarMax = reader.ReadSingle();
            ZoomScalarMin = reader.ReadSingle();
            ZoomScalarMax = reader.ReadSingle();
            ColourTintMin = reader.ReadUInt32();
            ColourTintMax = reader.ReadUInt32();
            Unknown_68h = reader.ReadUInt64();

            // read reference data
            UnknownData = reader.ReadBlockAt<ParticleUnknown1>(UnknownDataPointer);
            EmitterRuleName = reader.ReadBlockAt<string_r>(EmitterRuleNamePointer);
            ParticleRuleName = reader.ReadBlockAt<string_r>(ParticleRuleNamePointer);
            EmitterRule = reader.ReadBlockAt<ParticleEmitterRule>(EmitterRulePointer);
            ParticleRule = reader.ReadBlockAt<ParticleRule>(ParticleRulePointer);


            if (!string.IsNullOrEmpty(EmitterRuleName?.Value))
            {
                JenkIndex.Ensure(EmitterRuleName.Value);
            }
            if (!string.IsNullOrEmpty(ParticleRuleName?.Value))
            {
                JenkIndex.Ensure(ParticleRuleName.Value);
            }

            if (EmitterRuleName?.Value != EmitterRule?.Name?.Value)
            { }//no hit
            if (ParticleRuleName?.Value != ParticleRule?.Name?.Value)
            { }//no hit

            //if (Unknown_4h != 1)
            //{ }//no hit
            //switch (Unknown_8h)
            //{
            //    case 0:
            //    case 1:
            //    case 2:
            //    case 3:
            //    case 4:
            //    case 5:
            //    case 6:
            //        break;
            //    default:
            //        break;//no hit
            //}
            //if (Unknown_Ch != 0)
            //{ }//no hit
            switch (Unknown_10h)
            {
                case 0:
                case 0.015f:
                case 0.1f:
                case 0.3f:
                case 0.8f:
                    break;
                default:
                    break;//more
            }
            switch (Unknown_14h)
            {
                case 1.0f:
                case 0.15f:
                case 0.01f:
                case 0.1f:
                case 0.3f:
                    break;
                default:
                    break;//more
            }
            //if (Unknown_20h != 0)
            //{ }//no hit
            //if (Unknown_28h != 0)
            //{ }//no hit
            switch (PlaybackRateScalarMin)
            {
                case 1.0f:
                case 2.0f:
                case 1.2f:
                case 0.8f:
                    break;
                default:
                    break;//more
            }
            switch (PlaybackRateScalarMax)
            {
                case 1.0f:
                case 2.0f:
                case 1.2f:
                case 0.8f:
                    break;
                default:
                    break;//and more
            }
            switch (ZoomScalarMin)
            {
                case 1.0f:
                case 0.5f:
                case 0.95f:
                case 1.2f:
                case 0.4f:
                    break;
                default:
                    break;//more
            }
            switch (ZoomScalarMax)
            {
                case 1.0f:
                case 1.2f:
                case 0.5f:
                case 0.4f:
                    break;
                default:
                    break;//more
            }
            switch (ColourTintMin)
            {
                case 0xffffffff:
                case 0xfffafafa:
                case 0xb4ffffff:
                case 0xffffdcc8:
                case 0xc8ffdcc8:
                case 0x5affffff:
                case 0xfffff2d1:
                case 0xc8ffffff:
                    break;
                default:
                    break;//more
            }
            switch (ColourTintMax)
            {
                case 0xffffffff:
                case 0xffffefc2:
                case 0x32ffffff:
                case 0x78ffa680:
                case 0x50ffa680:
                case 0x96f7b068:
                case 0x5affffff:
                case 0xa0ffd280:
                case 0xb4ffffff:
                case 0xffffebba:
                case 0xffffb47a:
                case 0xbeffffff:
                    break;
                default:
                    break;//more
            }
            //if (Unknown_68h != 0)
            //{ }//no hit

        }
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            // update structure data
            UnknownDataPointer = (ulong)(UnknownData != null ? UnknownData.FilePosition : 0);
            EmitterRuleNamePointer = (ulong)(EmitterRuleName != null ? EmitterRuleName.FilePosition : 0);
            ParticleRuleNamePointer = (ulong)(ParticleRuleName != null ? ParticleRuleName.FilePosition : 0);
            EmitterRulePointer = (ulong)(EmitterRule != null ? EmitterRule.FilePosition : 0);
            ParticleRulePointer = (ulong)(ParticleRule != null ? ParticleRule.FilePosition : 0);

            // write structure data
            writer.Write(VFT);
            writer.Write(Unknown_4h);
            writer.Write(Index);
            writer.Write(Unknown_Ch);
            writer.Write(Unknown_10h);
            writer.Write(Unknown_14h);
            writer.Write(UnknownDataPointer);
            writer.Write(Unknown_20h);
            writer.Write(Unknown_28h);
            writer.Write(EmitterRuleNamePointer);
            writer.Write(ParticleRuleNamePointer);
            writer.Write(EmitterRulePointer);
            writer.Write(ParticleRulePointer);
            writer.Write(PlaybackRateScalarMin);
            writer.Write(PlaybackRateScalarMax);
            writer.Write(ZoomScalarMin);
            writer.Write(ZoomScalarMax);
            writer.Write(ColourTintMin);
            writer.Write(ColourTintMax);
            writer.Write(Unknown_68h);
        }
        public void WriteXml(StringBuilder sb, int indent)
        {
            YptXml.StringTag(sb, indent, "EmitterRule", YptXml.XmlEscape(EmitterRuleName?.Value ?? ""));
            YptXml.StringTag(sb, indent, "ParticleRule", YptXml.XmlEscape(ParticleRuleName?.Value ?? ""));
            YptXml.ValueTag(sb, indent, "Unknown10", FloatUtil.ToString(Unknown_10h));
            YptXml.ValueTag(sb, indent, "Unknown14", FloatUtil.ToString(Unknown_14h));
            YptXml.ValueTag(sb, indent, "PlaybackRateScalarMin", FloatUtil.ToString(PlaybackRateScalarMin));
            YptXml.ValueTag(sb, indent, "PlaybackRateScalarMax", FloatUtil.ToString(PlaybackRateScalarMax));
            YptXml.ValueTag(sb, indent, "ZoomScalarMin", FloatUtil.ToString(ZoomScalarMin));
            YptXml.ValueTag(sb, indent, "ZoomScalarMax", FloatUtil.ToString(ZoomScalarMax));
            YptXml.ValueTag(sb, indent, "ColourTintMin", YptXml.UintString(ColourTintMin));
            YptXml.ValueTag(sb, indent, "ColourTintMax", YptXml.UintString(ColourTintMax));
            if (UnknownData != null)
            {
                YptXml.OpenTag(sb, indent, "UnknownData");
                UnknownData.WriteXml(sb, indent + 1);
                YptXml.CloseTag(sb, indent, "UnknownData");
            }
        }
        public void ReadXml(XmlNode node)
        {
            EmitterRuleName = (string_r)Xml.GetChildInnerText(node, "EmitterRule"); if (EmitterRuleName.Value == null) EmitterRuleName = null;
            ParticleRuleName = (string_r)Xml.GetChildInnerText(node, "ParticleRule"); if (ParticleRuleName.Value == null) ParticleRuleName = null;
            Unknown_10h = Xml.GetChildFloatAttribute(node, "Unknown10");
            Unknown_14h = Xml.GetChildFloatAttribute(node, "Unknown14");
            PlaybackRateScalarMin = Xml.GetChildFloatAttribute(node, "PlaybackRateScalarMin");
            PlaybackRateScalarMax = Xml.GetChildFloatAttribute(node, "PlaybackRateScalarMax");
            ZoomScalarMin = Xml.GetChildFloatAttribute(node, "ZoomScalarMin");
            ZoomScalarMax = Xml.GetChildFloatAttribute(node, "ZoomScalarMax");
            ColourTintMin = Xml.GetChildUIntAttribute(node, "ColourTintMin");
            ColourTintMax = Xml.GetChildUIntAttribute(node, "ColourTintMax");
            var udnode = node.SelectSingleNode("UnknownData");
            if (udnode != null)
            {
                UnknownData = new ParticleUnknown1();
                UnknownData.ReadXml(udnode);
            }
        }

        public override IResourceBlock[] GetReferences()
        {
            var list = new List<IResourceBlock>();
            if (UnknownData != null) list.Add(UnknownData);
            if (EmitterRuleName != null) list.Add(EmitterRuleName);
            if (ParticleRuleName != null) list.Add(ParticleRuleName);
            if (EmitterRule != null) list.Add(EmitterRule);
            if (ParticleRule != null) list.Add(ParticleRule);
            return list.ToArray();
        }

        public override string ToString()
        {
            return EmitterRuleName?.ToString() ?? ParticleRuleName?.ToString() ?? base.ToString();
        }

    }


    [TC(typeof(EXP))] public class ParticleUnknown1 : ResourceSystemBlock
    {
        public override long BlockLength => 0x40;

        // structure data
        public ResourceSimpleList64<ParticleStringBlock> EventEmitterFlags { get; set; }
        public ResourceSimpleList64<ParticleUnknown2> Unknown_10h { get; set; }
        public ulong Unknown_20h = 1; // 0x0000000000000001
        public ResourceSimpleList64<ParticleUnknown2Block> Unknown_28h { get; set; }
        public ulong Unknown_38h; // 0x0000000000000000

        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            // read structure data
            EventEmitterFlags = reader.ReadBlock<ResourceSimpleList64<ParticleStringBlock>>();
            Unknown_10h = reader.ReadBlock<ResourceSimpleList64<ParticleUnknown2>>();
            Unknown_20h = reader.ReadUInt64();
            Unknown_28h = reader.ReadBlock<ResourceSimpleList64<ParticleUnknown2Block>>();
            Unknown_38h = reader.ReadUInt64();

            //if (Unknown_20h != 1)
            //{ }//no hit
            //if (Unknown_38h != 0)
            //{ }//no hit

            var cnt1 = (EventEmitterFlags?.data_items?.Length ?? 0);
            var cnt2 = (Unknown_10h?.data_items?.Length ?? 0);
            var cnt3 = (Unknown_28h?.data_items?.Length ?? 0);

            if (cnt2 != cnt3)
            { }//no hit
            if ((cnt2 != 0) && (cnt2 != cnt1))
            { }//hit
            if ((cnt3 != 0) && (cnt3 != cnt1))
            { }//hit


            //var dic = new Dictionary<MetaHash, ParticleUnknown2>();
            //if (Unknown_10h?.data_items != null)
            //{
            //    foreach (var item in Unknown_10h.data_items)
            //    {
            //        dic[item.NameHash] = item;
            //    }
            //}
            //if (Unknown_28h?.data_items != null)
            //{
            //    MetaHash lasthash = 0;
            //    foreach (var item in Unknown_28h.data_items)
            //    {
            //        if (item.NameHash < lasthash)
            //        { }//no hit! - this array is a sorted dictionary of the items!
            //        lasthash = item.NameHash;
            //        if (dic.TryGetValue(item.NameHash, out ParticleUnknown2 oitem))
            //        {
            //            if (item.Item != oitem)
            //            { }//no hit
            //        }
            //        else
            //        { }//no hit
            //    }
            //}


        }
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            // write structure data
            writer.WriteBlock(EventEmitterFlags);
            writer.WriteBlock(Unknown_10h);
            writer.Write(Unknown_20h);
            writer.WriteBlock(Unknown_28h);
            writer.Write(Unknown_38h);
        }
        public void WriteXml(StringBuilder sb, int indent)
        {
            if (EventEmitterFlags?.data_items != null)
            {
                if (EventEmitterFlags.data_items.Length > 0)
                {
                    YptXml.OpenTag(sb, indent, "EventEmitterFlags");
                    foreach (var item in EventEmitterFlags.data_items)
                    {
                        YptXml.StringTag(sb, indent + 1, "Item", YptXml.XmlEscape(item?.Name?.Value ?? ""));
                    }
                    YptXml.CloseTag(sb, indent, "EventEmitterFlags");
                }
                else
                {
                    YptXml.SelfClosingTag(sb, indent, "EventEmitterFlags");
                }
            }
            if (Unknown_10h?.data_items != null)
            {
                YptXml.WriteItemArray(sb, Unknown_10h.data_items, indent, "Unknown10");
            }
            //if (Unknown_28h?.data_items != null)
            //{
            //    YptXml.WriteItemArray(sb, Unknown_28h.data_items, indent, "Unknown28");
            //}
        }
        public void ReadXml(XmlNode node)
        {
            EventEmitterFlags = new ResourceSimpleList64<ParticleStringBlock>();
            //EventEmitterFlags.data_items = XmlMeta.ReadItemArray<ParticleStringBlock>(node, "EventEmitterFlags");
            var unode = node.SelectSingleNode("EventEmitterFlags");
            if (unode != null)
            {
                var inodes = unode.SelectNodes("Item");
                var ilist = new List<ParticleStringBlock>();
                foreach (XmlNode inode in inodes)
                {
                    var iname = inode.InnerText;
                    var blk = new ParticleStringBlock();
                    blk.Name = (string_r)iname;
                    ilist.Add(blk);
                }
                EventEmitterFlags.data_items = ilist.ToArray();
            }

            Unknown_10h = new ResourceSimpleList64<ParticleUnknown2>();
            Unknown_10h.data_items = XmlMeta.ReadItemArray<ParticleUnknown2>(node, "Unknown10");

            Unknown_28h = new ResourceSimpleList64<ParticleUnknown2Block>();
            //Unknown_28h.data_items = XmlMeta.ReadItemArray<ParticleUnknown2Block>(node, "Unknown28");
            if (Unknown_10h.data_items != null)
            {
                var blist = new List<ParticleUnknown2Block>();
                foreach (var item in Unknown_10h.data_items)
                {
                    var blk = new ParticleUnknown2Block();
                    blk.Item = item;
                    blk.Name = item.Name;
                    blist.Add(blk);
                }
                blist.Sort((a, b) => a.Name.Hash.CompareTo(b.Name.Hash));
                Unknown_28h.data_items = blist.ToArray();
            }
        }

        public override Tuple<long, IResourceBlock>[] GetParts()
        {
            return new Tuple<long, IResourceBlock>[] {
                new Tuple<long, IResourceBlock>(0, EventEmitterFlags),
                new Tuple<long, IResourceBlock>(0x10, Unknown_10h),
                new Tuple<long, IResourceBlock>(0x28, Unknown_28h)
            };
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }


    [TC(typeof(EXP))] public class ParticleStringBlock : ResourceSystemBlock
    {
        public override long BlockLength => 24;

        // structure data
        public ulong NamePointer { get; set; }
        public ulong Unknown_8h; // 0x0000000000000000
        public ulong Unknown_10h; // 0x0000000000000000

        // reference data
        public string_r Name { get; set; }

        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            // read structure data
            NamePointer = reader.ReadUInt64();
            Unknown_8h = reader.ReadUInt64();
            Unknown_10h = reader.ReadUInt64();

            // read reference data
            Name = reader.ReadBlockAt<string_r>(NamePointer);

            //if (!string.IsNullOrEmpty(String1?.Value))
            //{
            //    JenkIndex.Ensure(String1.Value);
            //}


            //if (Unknown_8h != 0)
            //{ }//no hit
            //if (Unknown_10h != 0)
            //{ }//no hit
        }
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            // update structure data
            NamePointer = (ulong)(Name != null ? Name.FilePosition : 0);

            // write structure data
            writer.Write(NamePointer);
            writer.Write(Unknown_8h);
            writer.Write(Unknown_10h);
        }

        public override IResourceBlock[] GetReferences()
        {
            var list = new List<IResourceBlock>();
            if (Name != null) list.Add(Name);
            return list.ToArray();
        }

        public override string ToString()
        {
            return Name?.ToString() ?? base.ToString();
        }
    }


    [TC(typeof(EXP))] public class ParticleUnknown2Block : ResourceSystemBlock
    {
        public override long BlockLength => 0x10;

        // structure data
        public ParticleKeyframePropName Name { get; set; }
        public uint Unknown_4h; // 0x00000000
        public ulong ItemPointer { get; set; }

        // reference data
        public ParticleUnknown2 Item { get; set; }

        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            // read structure data
            Name = reader.ReadUInt32();
            Unknown_4h = reader.ReadUInt32();
            ItemPointer = reader.ReadUInt64();

            // read reference data
            Item = reader.ReadBlockAt<ParticleUnknown2>(ItemPointer);

            if (Item != null)
            { }
            if ((Item?.Name ?? 0) != Name)
            { }//no hit! so this is just a "dictionary" entry for an Item!

            //if (Unknown_4h != 0)
            //{ }//no hit

        }
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            // update structure data
            ItemPointer = (ulong)(Item != null ? Item.FilePosition : 0);

            // write structure data
            writer.Write(Name);
            writer.Write(Unknown_4h);
            writer.Write(ItemPointer);
        }

        public override string ToString()
        {
            return Name.ToString();
        }

    }


    [TC(typeof(EXP))] public class ParticleUnknown2 : ResourceSystemBlock, IMetaXmlItem
    {
        public override long BlockLength => 24;

        // structure data
        public ResourceSimpleList64<ParticleUnknown3> Unknown_0h { get; set; }
        public ParticleKeyframePropName Name { get; set; }
        public uint Unknown_14h { get; set; } // 0, 1

        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            // read structure data
            Unknown_0h = reader.ReadBlock<ResourceSimpleList64<ParticleUnknown3>>();
            Name = reader.ReadUInt32();
            Unknown_14h = reader.ReadUInt32();

            //switch (Unknown_14h)
            //{
            //    case 1:
            //    case 0:
            //        break;
            //    default:
            //        break;//no hit
            //}
        }
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            // write structure data
            writer.WriteBlock(Unknown_0h);
            writer.Write(Name);
            writer.Write(Unknown_14h);
        }
        public void WriteXml(StringBuilder sb, int indent)
        {
            YptXml.StringTag(sb, indent, "Name", Name.ToString());
            YptXml.ValueTag(sb, indent, "Unknown14", Unknown_14h.ToString());
            if (Unknown_0h?.data_items != null)
            {
                YptXml.WriteItemArray(sb, Unknown_0h.data_items, indent, "Items");
            }
        }
        public void ReadXml(XmlNode node)
        {
            Name = Xml.GetChildInnerText(node, "Name");
            Unknown_14h = Xml.GetChildUIntAttribute(node, "Unknown14");
            Unknown_0h = new ResourceSimpleList64<ParticleUnknown3>();
            Unknown_0h.data_items = XmlMeta.ReadItemArray<ParticleUnknown3>(node, "Items");
        }

        public override Tuple<long, IResourceBlock>[] GetParts()
        {
            return new Tuple<long, IResourceBlock>[] {
                new Tuple<long, IResourceBlock>(0, Unknown_0h)
            };
        }

        public override string ToString()
        {
            return Name.ToString();
        }

    }


    [TC(typeof(EXP))] public class ParticleUnknown3 : ResourceSystemBlock, IMetaXmlItem
    {
        public override long BlockLength => 0x30;

        // structure data
        public ResourceSimpleList64<ParticleKeyframePropValue> Unknown_0h { get; set; }
        public ulong Unknown_10h; // 0x0000000000000000
        public ulong Unknown_18h; // 0x0000000000000000
        public uint Unknown_20h { get; set; } // 0, 1, 2, 3, 4
        public uint Unknown_24h { get; set; } // 0, 1
        public ulong Unknown_28h; // 0x0000000000000000

        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            // read structure data
            Unknown_0h = reader.ReadBlock<ResourceSimpleList64<ParticleKeyframePropValue>>();
            Unknown_10h = reader.ReadUInt64();
            Unknown_18h = reader.ReadUInt64();
            Unknown_20h = reader.ReadUInt32();
            Unknown_24h = reader.ReadUInt32();
            Unknown_28h = reader.ReadUInt64();

            //if (Unknown_10h != 0)
            //{ }//no hit
            //if (Unknown_18h != 0)
            //{ }//no hit
            //switch (Unknown_20h)
            //{
            //    case 3:
            //    case 2:
            //    case 1:
            //    case 0:
            //    case 4:
            //        break;
            //    default:
            //        break;//no hit
            //}
            //switch (Unknown_24h)
            //{
            //    case 0:
            //    case 1:
            //        break;
            //    default:
            //        break;//no hit
            //}
            //if (Unknown_28h != 0)
            //{ }//no hit
        }
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            // write structure data
            writer.WriteBlock(Unknown_0h);
            writer.Write(Unknown_10h);
            writer.Write(Unknown_18h);
            writer.Write(Unknown_20h);
            writer.Write(Unknown_24h);
            writer.Write(Unknown_28h);
        }
        public void WriteXml(StringBuilder sb, int indent)
        {
            YptXml.ValueTag(sb, indent, "Unknown20", Unknown_20h.ToString());
            YptXml.ValueTag(sb, indent, "Unknown24", Unknown_24h.ToString());
            if (Unknown_0h?.data_items != null)
            {
                YptXml.WriteItemArray(sb, Unknown_0h.data_items, indent, "Keyframes");
            }
        }
        public void ReadXml(XmlNode node)
        {
            Unknown_20h = Xml.GetChildUIntAttribute(node, "Unknown20");
            Unknown_24h = Xml.GetChildUIntAttribute(node, "Unknown24");
            Unknown_0h = new ResourceSimpleList64<ParticleKeyframePropValue>();
            Unknown_0h.data_items = XmlMeta.ReadItemArray<ParticleKeyframePropValue>(node, "Keyframes");
        }

        public override Tuple<long, IResourceBlock>[] GetParts()
        {
            return new Tuple<long, IResourceBlock>[] {
                new Tuple<long, IResourceBlock>(0, Unknown_0h)
            };
        }

        public override string ToString()
        {
            return Unknown_20h.ToString() + ", " + Unknown_24h.ToString();
        }

    }










    [TC(typeof(EXP))] public class ParticleEmitterRule : ResourceSystemBlock
    {
        // pgBase
        // pgBaseRefCounted
        // ptxEmitterRule
        public override long BlockLength => 0x630;

        // structure data
        public uint VFT { get; set; }
        public uint Unknown_4h = 1; // 0x00000001
        public ulong Unknown_8h; // 0x0000000000000000
        public uint Unknown_10h { get; set; } // 2, 3, 4, 5, 6, 10, 21
        public uint Unknown_14h; // 0x00000000
        public float Unknown_18h { get; set; } = 4.1f; // 4.1f
        public uint Unknown_1Ch; // 0x00000000
        public ulong NamePointer { get; set; }
        public ulong Unknown_28h; // 0x0000000000000000
        public ulong Unknown_30h; // 0x0000000000000000
        public ulong Domain1Pointer { get; set; }
        public ulong Unknown_40h; // 0x0000000000000000
        public ulong Domain2Pointer { get; set; }
        public ulong Unknown_50h; // 0x0000000000000000
        public ulong Domain3Pointer { get; set; }
        public ulong Unknown_60h; // 0x0000000000000000
        public ulong Unknown_68h; // 0x0000000000000000
        public ulong Unknown_70h; // 0x0000000000000000
        public ParticleKeyframeProp[] KeyframeProps1 { get; set; } = new ParticleKeyframeProp[10];
        public ulong KeyframeProps2Pointer { get; set; }
        public ushort KeyframePropsCount1 = 10; // 10
        public ushort KeyframePropsCount2 = 10; // 10
        public uint Unknown_624h; // 0x00000000
        public uint Unknown_628h { get; set; } // 0, 1
        public uint Unknown_62Ch; // 0x00000000

        // reference data
        public string_r Name { get; set; }
        public MetaHash NameHash { get; set; }
        public ParticleDomain Domain1 { get; set; }
        public ParticleDomain Domain2 { get; set; }
        public ParticleDomain Domain3 { get; set; }
        public ResourcePointerArray64<ParticleKeyframeProp> KeyframeProps2 { get; set; }//just pointers to KeyframeProps1

        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            // read structure data
            VFT = reader.ReadUInt32();
            Unknown_4h = reader.ReadUInt32();
            Unknown_8h = reader.ReadUInt64();
            Unknown_10h = reader.ReadUInt32();
            Unknown_14h = reader.ReadUInt32();
            Unknown_18h = reader.ReadSingle();
            Unknown_1Ch = reader.ReadUInt32();
            NamePointer = reader.ReadUInt64();
            Unknown_28h = reader.ReadUInt64();
            Unknown_30h = reader.ReadUInt64();
            Domain1Pointer = reader.ReadUInt64();
            Unknown_40h = reader.ReadUInt64();
            Domain2Pointer = reader.ReadUInt64();
            Unknown_50h = reader.ReadUInt64();
            Domain3Pointer = reader.ReadUInt64();
            Unknown_60h = reader.ReadUInt64();
            Unknown_68h = reader.ReadUInt64();
            Unknown_70h = reader.ReadUInt64();
            for (int i = 0; i < 10; i++)
            {
                KeyframeProps1[i] = reader.ReadBlock<ParticleKeyframeProp>();
            }
            KeyframeProps2Pointer = reader.ReadUInt64();
            KeyframePropsCount1 = reader.ReadUInt16();
            KeyframePropsCount2 = reader.ReadUInt16();
            Unknown_624h = reader.ReadUInt32();
            Unknown_628h = reader.ReadUInt32();
            Unknown_62Ch = reader.ReadUInt32();

            // read reference data
            Name = reader.ReadBlockAt<string_r>(NamePointer);
            Domain1 = reader.ReadBlockAt<ParticleDomain>(Domain1Pointer);
            Domain2 = reader.ReadBlockAt<ParticleDomain>(Domain2Pointer);
            Domain3 = reader.ReadBlockAt<ParticleDomain>(Domain3Pointer);
            KeyframeProps2 = reader.ReadBlockAt<ResourcePointerArray64<ParticleKeyframeProp>>(KeyframeProps2Pointer, KeyframePropsCount2);


            if (!string.IsNullOrEmpty(Name?.Value))
            {
                JenkIndex.Ensure(Name.Value);
            }

            //if ((Domain1 != null) && (Domain1.Index != 0))
            //{ }//no hit
            //if ((Domain2 != null) && (Domain2.Index != 1))
            //{ }//no hit
            //if ((Domain3 != null) && (Domain3.Index != 2))
            //{ }//no hit

            //if (KeyframeProps2?.data_items != null)
            //{
            //    if (KeyframeProps2.data_items.Length != 10)
            //    { }//no hit
            //    else
            //    {
            //        for (int i = 0; i < 10; i++)
            //        {
            //            if (KeyframeProps2.data_items[i] != KeyframeProps1[i])
            //            { }//no hit
            //        }
            //    }
            //}
            //else
            //{ }//no hit

            //if (Unknown_4h != 1)
            //{ }//no hit
            //if (Unknown_8h != 0)
            //{ }//no hit
            //switch (Unknown_10h)
            //{
            //    case 3:
            //    case 2:
            //    case 4:
            //    case 5:
            //    case 10:
            //    case 21:
            //    case 6:
            //        break;
            //    default:
            //        break;//no hit
            //}
            //if (Unknown_14h != 0)
            //{ }//no hit
            //if (Unknown_18h != 4.1f)
            //{ }//no hit
            //if (Unknown_1Ch != 0)
            //{ }//no hit
            //if (Unknown_28h != 0)
            //{ }//no hit
            //if (Unknown_30h != 0)
            //{ }//no hit
            //if (Unknown_40h != 0)
            //{ }//no hit
            //if (Unknown_50h != 0)
            //{ }//no hit
            //if (Unknown_60h != 0)
            //{ }//no hit
            //if (Unknown_68h != 0)
            //{ }//no hit
            //if (Unknown_70h != 0)
            //{ }//no hit
            //if (KeyframePropsCount1 != 10)
            //{ }//no hit
            //if (KeyframePropsCount2 != 10)
            //{ }//no hit
            //if (Unknown_624h != 0)
            //{ }//no hit
            //switch (Unknown_628h)
            //{
            //    case 0:
            //    case 1:
            //        break;
            //    default:
            //        break;//no hit
            //}
            //if (Unknown_62Ch != 0)
            //{ }//no hit
        }
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            // update structure data
            NamePointer = (ulong)(Name != null ? Name.FilePosition : 0);
            Domain1Pointer = (ulong)(Domain1 != null ? Domain1.FilePosition : 0);
            Domain2Pointer = (ulong)(Domain2 != null ? Domain2.FilePosition : 0);
            Domain3Pointer = (ulong)(Domain3 != null ? Domain3.FilePosition : 0);
            KeyframeProps2Pointer = (ulong)(KeyframeProps2 != null ? KeyframeProps2.FilePosition : 0);
            //refcnt2 = (ushort)(refs != null ? refs.Count : 0);


            // write structure data
            writer.Write(VFT);
            writer.Write(Unknown_4h);
            writer.Write(Unknown_8h);
            writer.Write(Unknown_10h);
            writer.Write(Unknown_14h);
            writer.Write(Unknown_18h);
            writer.Write(Unknown_1Ch);
            writer.Write(NamePointer);
            writer.Write(Unknown_28h);
            writer.Write(Unknown_30h);
            writer.Write(Domain1Pointer);
            writer.Write(Unknown_40h);
            writer.Write(Domain2Pointer);
            writer.Write(Unknown_50h);
            writer.Write(Domain3Pointer);
            writer.Write(Unknown_60h);
            writer.Write(Unknown_68h);
            writer.Write(Unknown_70h);
            for (int i = 0; i < 10; i++)
            {
                writer.WriteBlock(KeyframeProps1[i]);
            }
            writer.Write(KeyframeProps2Pointer);
            writer.Write(KeyframePropsCount1);
            writer.Write(KeyframePropsCount2);
            writer.Write(Unknown_624h);
            writer.Write(Unknown_628h);
            writer.Write(Unknown_62Ch);
        }
        public void WriteXml(StringBuilder sb, int indent)
        {
            YptXml.StringTag(sb, indent, "Name", YptXml.XmlEscape(Name?.Value ?? ""));
            YptXml.ValueTag(sb, indent, "Unknown10", Unknown_10h.ToString());
            YptXml.ValueTag(sb, indent, "Unknown628", Unknown_628h.ToString());
            ParticleDomain.WriteXmlNode(Domain1, sb, indent, "Domain1");
            ParticleDomain.WriteXmlNode(Domain2, sb, indent, "Domain2");
            ParticleDomain.WriteXmlNode(Domain3, sb, indent, "Domain3");
            if (KeyframeProps1 != null)
            {
                YptXml.WriteItemArray(sb, KeyframeProps1, indent, "KeyframeProperties");
            }
        }
        public void ReadXml(XmlNode node)
        {
            Name = (string_r)Xml.GetChildInnerText(node, "Name"); if (Name.Value == null) Name = null;
            NameHash = JenkHash.GenHash(Name?.Value ?? "");
            Unknown_10h = Xml.GetChildUIntAttribute(node, "Unknown10");
            Unknown_628h = Xml.GetChildUIntAttribute(node, "Unknown628");
            Domain1 = ParticleDomain.ReadXmlNode(node.SelectSingleNode("Domain1")); if (Domain1 != null) Domain1.Index = 0;
            Domain2 = ParticleDomain.ReadXmlNode(node.SelectSingleNode("Domain2")); if (Domain2 != null) Domain2.Index = 1;
            Domain3 = ParticleDomain.ReadXmlNode(node.SelectSingleNode("Domain3")); if (Domain3 != null) Domain3.Index = 2;

            var kflist = XmlMeta.ReadItemArray<ParticleKeyframeProp>(node, "KeyframeProperties")?.ToList() ?? new List<ParticleKeyframeProp>();
            KeyframeProps1 = new ParticleKeyframeProp[10];
            for (int i = 0; i < 10; i++)
            {
                KeyframeProps1[i] = (i < kflist.Count) ? kflist[i] : new ParticleKeyframeProp();
            }

            KeyframeProps2 = new ResourcePointerArray64<ParticleKeyframeProp>();
            KeyframeProps2.data_items = KeyframeProps1;
            KeyframeProps2.ManualReferenceOverride = true;
        }

        public override IResourceBlock[] GetReferences()
        {
            var list = new List<IResourceBlock>();
            if (Name != null) list.Add(Name);
            if (Domain1 != null) list.Add(Domain1);
            if (Domain2 != null) list.Add(Domain2);
            if (Domain3 != null) list.Add(Domain3);
            if (KeyframeProps2 != null)
            {
                KeyframeProps2.ManualReferenceOverride = true;
                list.Add(KeyframeProps2);
            }
            return list.ToArray();
        }

        public override Tuple<long, IResourceBlock>[] GetParts()
        {
            return new Tuple<long, IResourceBlock>[] {
                new Tuple<long, IResourceBlock>(120, KeyframeProps1[0]),
                new Tuple<long, IResourceBlock>(264, KeyframeProps1[1]),
                new Tuple<long, IResourceBlock>(408, KeyframeProps1[2]),
                new Tuple<long, IResourceBlock>(552, KeyframeProps1[3]),
                new Tuple<long, IResourceBlock>(696, KeyframeProps1[4]),
                new Tuple<long, IResourceBlock>(840, KeyframeProps1[5]),
                new Tuple<long, IResourceBlock>(984, KeyframeProps1[6]),
                new Tuple<long, IResourceBlock>(1128, KeyframeProps1[7]),
                new Tuple<long, IResourceBlock>(1272, KeyframeProps1[8]),
                new Tuple<long, IResourceBlock>(1416, KeyframeProps1[9]),
            };
        }

        public override string ToString()
        {
            return Name?.ToString() ?? base.ToString();
        }
    }








    [TC(typeof(EXP))] public struct ParticleKeyframePropName
    {
        public uint Hash { get; set; }

        public ParticleKeyframePropName(uint h) { Hash = h; }
        public ParticleKeyframePropName(string str)
        {
            var strl = str?.ToLowerInvariant() ?? "";
            if (strl.StartsWith("hash_"))
            {
                Hash = Convert.ToUInt32(strl.Substring(5), 16);
            }
            else
            {
                Hash = JenkHash.GenHash(strl);
            }
        }

        public override string ToString()
        {
            var str = ParticleKeyframeProp.GetName(Hash);
            if (!string.IsNullOrEmpty(str)) return str;
            return YptXml.HashString((MetaHash)Hash);
        }

        public string ToCleanString()
        {
            if (Hash == 0) return string.Empty;
            return ToString();
        }

        public static implicit operator uint(ParticleKeyframePropName h)
        {
            return h.Hash;  //implicit conversion
        }

        public static implicit operator ParticleKeyframePropName(uint v)
        {
            return new ParticleKeyframePropName(v);
        }
        public static implicit operator ParticleKeyframePropName(string s)
        {
            return new ParticleKeyframePropName(s);
        }
    }


    [TC(typeof(EXP))] public class ParticleKeyframeProp : ResourceSystemBlock, IMetaXmlItem
    {
        // datBase
        // ptxKeyframeProp
        public override long BlockLength => 0x90;

        // structure data
        public uint VFT { get; set; }
        public uint Unknown_4h = 1; // 0x00000001
        public ulong Unknown_8h; // 0x0000000000000000
        public ulong Unknown_10h; // 0x0000000000000000
        public ulong Unknown_18h; // 0x0000000000000000
        public ulong Unknown_20h; // 0x0000000000000000
        public ulong Unknown_28h; // 0x0000000000000000
        public ulong Unknown_30h; // 0x0000000000000000
        public ulong Unknown_38h; // 0x0000000000000000
        public ulong Unknown_40h; // 0x0000000000000000
        public ulong Unknown_48h; // 0x0000000000000000
        public ulong Unknown_50h; // 0x0000000000000000
        public ulong Unknown_58h; // 0x0000000000000000
        public ulong Unknown_60h; // 0x0000000000000000
        public ParticleKeyframePropName Name { get; set; } // name hash?
        public uint Unknown_6Ch { get; set; } //offset..?
        public ResourceSimpleList64<ParticleKeyframePropValue> Values { get; set; }
        public ulong Unknown_80h; // 0x0000000000000000
        public ulong Unknown_88h; // 0x0000000000000000


        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            // read structure data
            VFT = reader.ReadUInt32();
            Unknown_4h = reader.ReadUInt32();
            Unknown_8h = reader.ReadUInt64();
            Unknown_10h = reader.ReadUInt64();
            Unknown_18h = reader.ReadUInt64();
            Unknown_20h = reader.ReadUInt64();
            Unknown_28h = reader.ReadUInt64();
            Unknown_30h = reader.ReadUInt64();
            Unknown_38h = reader.ReadUInt64();
            Unknown_40h = reader.ReadUInt64();
            Unknown_48h = reader.ReadUInt64();
            Unknown_50h = reader.ReadUInt64();
            Unknown_58h = reader.ReadUInt64();
            Unknown_60h = reader.ReadUInt64();
            Name = reader.ReadUInt32();
            Unknown_6Ch = reader.ReadUInt32();
            Values = reader.ReadBlock<ResourceSimpleList64<ParticleKeyframePropValue>>();
            Unknown_80h = reader.ReadUInt64();
            Unknown_88h = reader.ReadUInt64();


            //if (Unknown_4h != 1)
            //{ }//no hit
            //if (Unknown_8h != 0)
            //{ }//no hit
            //if (Unknown_10h != 0)
            //{ }//no hit
            //if (Unknown_18h != 0)
            //{ }//no hit
            //if (Unknown_20h != 0)
            //{ }//no hit
            //if (Unknown_28h != 0)
            //{ }//no hit
            //if (Unknown_30h != 0)
            //{ }//no hit
            //if (Unknown_38h != 0)
            //{ }//no hit
            //if (Unknown_40h != 0)
            //{ }//no hit
            //if (Unknown_48h != 0)
            //{ }//no hit
            //if (Unknown_50h != 0)
            //{ }//no hit
            //if (Unknown_58h != 0)
            //{ }//no hit
            //if (Unknown_60h != 0)
            //{ }//no hit
            switch (Unknown_6Ch)//some offset..?
            {
                case 0x00007a00:
                case 0x00007b00:
                case 0x00007c00:
                case 0x00007d00:
                case 0x00007e00:
                case 0x00007f00:
                case 0x00008000:
                case 0x00008100:
                case 0x00008200:
                case 0x00008300:
                case 0x0000e400:
                case 0x0000e500:
                case 0x0000e600:
                case 0x0000e700:
                case 0x0000e800:
                case 0x0000e900:
                case 0x0000ea00:
                case 0x0000eb00:
                case 0x0000ec00:
                case 0x0000ed00:
                case 0x0000ee00:
                case 0x0000ef00:
                case 0x0000f000:
                case 0x0000f100:
                case 0x0000f200:
                case 0x0000f300:
                case 0x0000f400:
                case 0x00000600:
                case 0x00000700:
                case 0x00000800:
                    break;
                default:
                    break;///and more......
            }
            //if (Unknown_80h != 0)
            //{ }//no hit
            //if (Unknown_88h != 0)
            //{ }//no hit
        }
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            // write structure data
            writer.Write(VFT);
            writer.Write(Unknown_4h);
            writer.Write(Unknown_8h);
            writer.Write(Unknown_10h);
            writer.Write(Unknown_18h);
            writer.Write(Unknown_20h);
            writer.Write(Unknown_28h);
            writer.Write(Unknown_30h);
            writer.Write(Unknown_38h);
            writer.Write(Unknown_40h);
            writer.Write(Unknown_48h);
            writer.Write(Unknown_50h);
            writer.Write(Unknown_58h);
            writer.Write(Unknown_60h);
            writer.Write(Name);
            writer.Write(Unknown_6Ch);
            writer.WriteBlock(Values);
            writer.Write(Unknown_80h);
            writer.Write(Unknown_88h);
        }
        public void WriteXml(StringBuilder sb, int indent)
        {
            YptXml.StringTag(sb, indent, "Name", Name.ToString());
            YptXml.ValueTag(sb, indent, "Unknown6C", Unknown_6Ch.ToString());

            if (Values?.data_items != null)
            {
                YptXml.WriteItemArray(sb, Values.data_items, indent, "Keyframes");
            }

        }
        public void ReadXml(XmlNode node)
        {
            Name = Xml.GetChildInnerText(node, "Name");
            Unknown_6Ch = Xml.GetChildUIntAttribute(node, "Unknown6C");

            Values = new ResourceSimpleList64<ParticleKeyframePropValue>();
            Values.data_items = XmlMeta.ReadItemArray<ParticleKeyframePropValue>(node, "Keyframes");

        }

        public override Tuple<long, IResourceBlock>[] GetParts()
        {
            return new Tuple<long, IResourceBlock>[] {
                new Tuple<long, IResourceBlock>(0x70, Values)
            };
        }

        public override string ToString()
        {
            return Name.ToString() + " (" + (Values?.data_items?.Length ?? 0).ToString() + " values)";
        }





        public static string GetName(uint hash)
        {
            if (NameDict == null)
            {
                //thanks to zirconium for this
                var d = new Dictionary<uint, string>();
                d[0x30e327d4] = "ptxu_Acceleration:m_xyzMinKFP"; 
                d[0x412a554c] = "ptxu_Acceleration:m_xyzMaxKFP"; 
                d[0x1f641348] = "ptxu_Size:m_whdMinKFP"; 
                d[0x3dc78098] = "ptxu_Size:m_whdMaxKFP"; 
                d[0xa67a1155] = "ptxu_Size:m_tblrScalarKFP"; 
                d[0xd5c0fce5] = "ptxu_Size:m_tblrVelScalarKFP"; 
                d[0xe7af1a2c] = "ptxu_MatrixWeight:m_mtxWeightKFP"; 
                d[0x7fae9df8] = "ptxu_Colour:m_rgbaMinKFP"; 
                d[0x60500691] = "ptxu_Colour:m_rgbaMaxKFP"; 
                d[0x8306b23a] = "ptxu_Colour:m_emissiveIntensityKFP"; 
                d[0x1c256ba4] = "ptxu_Rotation:m_initialAngleMinKFP"; 
                d[0x351ed852] = "ptxu_Rotation:m_initialAngleMaxKFP"; 
                d[0xf0274f77] = "ptxu_Rotation:m_angleMinKFP"; 
                d[0x687b4382] = "ptxu_Rotation:m_angleMaxKFP"; 
                d[0x61532d47] = "ptxu_Collision:m_bouncinessKFP"; 
                d[0x686f965f] = "ptxu_Collision:m_bounceDirVarKFP"; 
                d[0x2946e76f] = "ptxu_AnimateTexture:m_animRateKFP"; 
                d[0xd0ef73c5] = "ptxu_Dampening:m_xyzMinKFP"; 
                d[0x64c7fc25] = "ptxu_Dampening:m_xyzMaxKFP"; 
                d[0x0aadcbef] = "ptxu_Wind:m_influenceKFP"; 
                d[0xfb8eb4e6] = "ptxu_Decal:m_dimensionsKFP"; 
                d[0xa7228870] = "ptxu_Decal:m_alphaKFP"; 
                d[0xe5480b3b] = "ptxEffectRule:m_colourTintMinKFP"; 
                d[0xd7c1e22b] = "ptxEffectRule:m_colourTintMaxKFP"; 
                d[0xce8e57a7] = "ptxEffectRule:m_zoomScalarKFP"; 
                d[0x34d6ded7] = "ptxEffectRule:m_dataSphereKFP"; 
                d[0xff864d6c] = "ptxEffectRule:m_dataCapsuleKFP"; 
                d[0x61c50318] = "ptxEmitterRule:m_spawnRateOverTimeKFP"; 
                d[0xe00e5025] = "ptxEmitterRule:m_spawnRateOverDistKFP"; 
                d[0x9fc4652b] = "ptxEmitterRule:m_particleLifeKFP"; 
                d[0x60855078] = "ptxEmitterRule:m_playbackRateScalarKFP"; 
                d[0xc9fe6abb] = "ptxEmitterRule:m_speedScalarKFP"; 
                d[0x4af0ffa1] = "ptxEmitterRule:m_sizeScalarKFP"; 
                d[0xa83b53f0] = "ptxEmitterRule:m_accnScalarKFP"; 
                d[0xdd18b4f2] = "ptxEmitterRule:m_dampeningScalarKFP"; 
                d[0xe511bc23] = "ptxEmitterRule:m_matrixWeightScalarKFP"; 
                d[0xd2df1fa0] = "ptxEmitterRule:m_inheritVelocityKFP"; 
                d[0x45e377e9] = "ptxCreationDomain:m_positionKFP"; 
                d[0x5e692d43] = "ptxCreationDomain:m_rotationKFP"; 
                d[0x1104051e] = "ptxCreationDomain:m_sizeOuterKFP"; 
                d[0x841ab3da] = "ptxCreationDomain:m_sizeInnerKFP"; 
                d[0x41d49131] = "ptxTargetDomain:m_positionKFP"; 
                d[0x64c6c696] = "ptxTargetDomain:m_rotationKFP"; 
                d[0x13c0cac4] = "ptxTargetDomain:m_sizeOuterKFP"; 
                d[0xe7d61ff7] = "ptxTargetDomain:m_sizeInnerKFP"; 
                d[0xda8c99a6] = "ptxu_Light:m_rgbMinKFP"; 
                d[0x12bbe65e] = "ptxu_Light:m_rgbMaxKFP"; 
                d[0xef500a62] = "ptxu_Light:m_intensityKFP"; 
                d[0x75990186] = "ptxu_Light:m_rangeKFP"; 
                d[0xe364d5b2] = "ptxu_Light:m_coronaRgbMinKFP"; 
                d[0xf8561886] = "ptxu_Light:m_coronaRgbMaxKFP"; 
                d[0xe2c464a6] = "ptxu_Light:m_coronaIntensityKFP"; 
                d[0xc35aaf9b] = "ptxu_Light:m_coronaSizeKFP"; 
                d[0xb9410926] = "ptxu_Light:m_coronaFlareKFP"; 
                d[0xce9adbfd] = "ptxu_ZCull:m_heightKFP"; 
                d[0xea6afaba] = "ptxu_ZCull:m_fadeDistKFP"; 
                d[0x2d0d70b5] = "ptxu_Noise:m_posNoiseMinKFP"; 
                d[0xff31aaf3] = "ptxu_Noise:m_posNoiseMaxKFP"; 
                d[0xf256e579] = "ptxu_Noise:m_velNoiseMinKFP"; 
                d[0x513812a5] = "ptxu_Noise:m_velNoiseMaxKFP"; 
                d[0xd1be590a] = "ptxu_Acceleration:m_strengthKFP"; 
                d[0x72668c6f] = "ptxd_Trail:m_texInfoKFP"; 
                d[0x3c599207] = "ptxu_FogVolume:m_rgbTintMinKFP"; 
                d[0x23f55175] = "ptxu_FogVolume:m_rgbTintMaxKFP"; 
                d[0x3ee8e85e] = "ptxu_FogVolume:m_densityRangeKFP"; 
                d[0xdafe6982] = "ptxu_FogVolume:m_scaleMinKFP"; 
                d[0x5473d2fe] = "ptxu_FogVolume:m_scaleMaxKFP"; 
                d[0x9ef3ceec] = "ptxu_FogVolume:m_rotationMinKFP"; 
                d[0x570dc9cd] = "ptxu_FogVolume:m_rotationMaxKFP"; 
                d[0x68f00338] = "ptxAttractorDomain:m_positionKFP"; 
                d[0x8ace32c2] = "ptxAttractorDomain:m_rotationKFP"; 
                d[0xc248b5c9] = "ptxAttractorDomain:m_sizeOuterKFP"; 
                d[0x851d3d14] = "ptxAttractorDomain:m_sizeInnerKFP";
                NameDict = d;
            }
            if (NameDict.TryGetValue(hash, out string str))
            {
                return str;
            }
            return YptXml.HashString((MetaHash)hash);
        }
        private static Dictionary<uint, string> NameDict;


    }


    [TC(typeof(EXP))] public class ParticleKeyframePropValue : ResourceSystemBlock, IMetaXmlItem
    {
        public override long BlockLength => 0x20;

        // structure data
        public float InterpolationInterval { get; set; }
        public float KeyFrameMultiplier { get; set; }
        public ulong Unknown_8h; // 0x0000000000000000
        public float RedChannelColour { get; set; }
        public float GreenChannelColour { get; set; }
        public float BlueChannelColour { get; set; }
        public float AlphaChannelColour { get; set; }

        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            // read structure data
            InterpolationInterval = reader.ReadSingle();
            KeyFrameMultiplier = reader.ReadSingle();
            Unknown_8h = reader.ReadUInt64();
            RedChannelColour = reader.ReadSingle();
            GreenChannelColour = reader.ReadSingle();
            BlueChannelColour = reader.ReadSingle();
            AlphaChannelColour = reader.ReadSingle();

            switch (InterpolationInterval)
            {
                case 0:
                case 1.0f:
                case 0.6f:
                case 0.010234f:
                case 0.12f:
                case 0.8f:
                    break;
                default:
                    break; //and more..
            }
            switch (KeyFrameMultiplier)
            {
                case 0:
                case 1.0f:
                case 1.66666663f:
                case 97.7135f:
                case 8.333334f:
                case 1.47058821f:
                case 5.00000048f:
                    break;
                default:
                    break; //and more...
            }
            //if (Unknown_8h != 0)
            //{ }//no hit
            switch (RedChannelColour)
            {
                case 0:
                case 1.2f:
                case 5.0f:
                case 2.4f:
                case 7.0f:
                case 1.0f:
                case 0.6f:
                case 0.931395f:
                case 0.45f:
                case 0.55f:
                case 0.5f:
                    break;
                default:
                    break; //and more..
            }
            switch (GreenChannelColour)
            {
                case 0:
                case 1.2f:
                case 5.0f:
                case 2.4f:
                case 7.0f:
                case 1.0f:
                case 0.6f:
                case 0.73913f:
                case 0.3f:
                case 0.5f:
                    break;
                default:
                    break; //and more...
            }
            switch (BlueChannelColour)
            {
                case -0.8f:
                case -0.5f:
                case 0:
                case 1.0f:
                case 0.213439f:
                case 4.000001f:
                case 0.05f:
                    break;
                default:
                    break; //and more...
            }
            switch (AlphaChannelColour)
            {
                case 0:
                case 1.0f:
                case 0.669767f:
                case 0.945107f:
                case 0.798588f:
                case 0.03f:
                case 0.6f:
                    break;
                default:
                    break;// and more..
            }
        }
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            // write structure data
            writer.Write(InterpolationInterval);
            writer.Write(KeyFrameMultiplier);
            writer.Write(Unknown_8h);
            writer.Write(RedChannelColour);
            writer.Write(GreenChannelColour);
            writer.Write(BlueChannelColour);
            writer.Write(AlphaChannelColour);
        }
        public void WriteXml(StringBuilder sb, int indent)
        {
            YptXml.ValueTag(sb, indent, "InterpolationInterval", FloatUtil.ToString(InterpolationInterval));
            YptXml.ValueTag(sb, indent, "KeyFrameMultiplier", FloatUtil.ToString(KeyFrameMultiplier));
            YptXml.ValueTag(sb, indent, "RedChannelColour", FloatUtil.ToString(RedChannelColour));
            YptXml.ValueTag(sb, indent, "GreenChannelColour", FloatUtil.ToString(GreenChannelColour));
            YptXml.ValueTag(sb, indent, "BlueChannelColour", FloatUtil.ToString(BlueChannelColour));
            YptXml.ValueTag(sb, indent, "AlphaChannelColour", FloatUtil.ToString(AlphaChannelColour));
        }
        public void ReadXml(XmlNode node)
        {
            InterpolationInterval = Xml.GetChildFloatAttribute(node, "InterpolationInterval");
            KeyFrameMultiplier = Xml.GetChildFloatAttribute(node, "KeyFrameMultiplier");
            RedChannelColour = Xml.GetChildFloatAttribute(node, "RedChannelColour");
            GreenChannelColour = Xml.GetChildFloatAttribute(node, "GreenChannelColour");
            BlueChannelColour = Xml.GetChildFloatAttribute(node, "BlueChannelColour");
            AlphaChannelColour = Xml.GetChildFloatAttribute(node, "AlphaChannelColour");
        }

        public override string ToString()
        {
            return string.Format("{0}, {1}, {2}, {3}, {4}, {5}", InterpolationInterval, KeyFrameMultiplier, RedChannelColour, GreenChannelColour, BlueChannelColour, AlphaChannelColour);
        }

    }












    public enum ParticleDomainType : byte
    {
        Box = 0,
        Sphere = 1,
        Cylinder = 2,
        Attractor = 3,
    }

    [TC(typeof(EXP))] public class ParticleDomain : ResourceSystemBlock, IResourceXXSystemBlock, IMetaXmlItem
    {
        // datBase
        // ptxDomain
        public override long BlockLength => 0x280;

        // structure data
        public uint VFT { get; set; }
        public uint Unknown_4h = 1; // 0x00000001
        public uint Index { get; set; } // 0, 1, 2   - index of this domain in the ParticleEmitterRule
        public ParticleDomainType DomainType { get; set; }
        public byte Unknown_Dh; // 0x00
        public ushort Unknown_Eh; // 0x0000
        public uint Unknown_10h { get; set; } // eg. 0x00010100
        public uint Unknown_14h; // 0x00000000
        public ParticleKeyframeProp KeyframeProp0 { get; set; }
        public ParticleKeyframeProp KeyframeProp1 { get; set; }
        public ParticleKeyframeProp KeyframeProp2 { get; set; }
        public ParticleKeyframeProp KeyframeProp3 { get; set; }
        public float Unknown_258h { get; set; } // -1.0f, 2.0f, 2.1f
        public uint Unknown_25Ch; // 0x00000000
        public ResourcePointerList64<ParticleKeyframeProp> KeyframeProps { get; set; }
        public ulong Unknown_270h; // 0x0000000000000000
        public ulong Unknown_278h; // 0x0000000000000000

        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            // read structure data
            VFT = reader.ReadUInt32();
            Unknown_4h = reader.ReadUInt32();
            Index = reader.ReadUInt32();
            DomainType = (ParticleDomainType)reader.ReadByte();
            Unknown_Dh = reader.ReadByte();
            Unknown_Eh = reader.ReadUInt16();
            Unknown_10h = reader.ReadUInt32();
            Unknown_14h = reader.ReadUInt32();
            KeyframeProp0 = reader.ReadBlock<ParticleKeyframeProp>();
            KeyframeProp1 = reader.ReadBlock<ParticleKeyframeProp>();
            KeyframeProp2 = reader.ReadBlock<ParticleKeyframeProp>();
            KeyframeProp3 = reader.ReadBlock<ParticleKeyframeProp>();
            Unknown_258h = reader.ReadSingle();
            Unknown_25Ch = reader.ReadUInt32();
            KeyframeProps = reader.ReadBlock<ResourcePointerList64<ParticleKeyframeProp>>();
            Unknown_270h = reader.ReadUInt64();
            Unknown_278h = reader.ReadUInt64();


            //if (KeyframeProps?.data_items?.Length != 4)
            //{ }//no hit
            //else
            //{
            //    if (KeyframeProps.data_items[0] != KeyframeProp0)
            //    { }//no hit
            //    if (KeyframeProps.data_items[1] != KeyframeProp1)
            //    { }//no hit
            //    if (KeyframeProps.data_items[3] != KeyframeProp2)
            //    { }//no hit - note stupid ordering
            //    if (KeyframeProps.data_items[2] != KeyframeProp3)
            //    { }//no hit - note stupid ordering
            //}
            //if (KeyframeProps?.EntriesCapacity != 16)
            //{ }//no hit  ... how to handle this when saving???



            //if (Unknown_4h != 1)
            //{ }//no hit
            //switch (Index)
            //{
            //    case 0:
            //    case 1:
            //    case 2:
            //        break;
            //    default:
            //        break;//no hit
            //}
            //if (Unknown_Dh != 0)
            //{ }//no hit
            //if (Unknown_Eh != 0)
            //{ }//no hit
            //switch (Unknown_10h)
            //{
            //    case 0:
            //    case 0x00000100:
            //    case 0x00000101:
            //    case 1:
            //    case 0x00010001:
            //    case 0x00010000:
            //    case 0x00010100:
            //    case 0x00010101:
            //        break;
            //    default:
            //        break;//no hit
            //}
            //if (Unknown_14h != 0)
            //{ }//no hit
            //switch (Unknown_258h)
            //{
            //    case 2.0f:
            //    case 2.1f:
            //    case -1.0f:
            //        break;
            //    default:
            //        break;//no hit
            //}
            //if (Unknown_25Ch != 0)
            //{ }//no hit
            //if (Unknown_270h != 0)
            //{ }//no hit
            //if (Unknown_278h != 0)
            //{ }//no hit

        }
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            // write structure data
            writer.Write(VFT);
            writer.Write(Unknown_4h);
            writer.Write(Index);
            writer.Write((byte)DomainType);
            writer.Write(Unknown_Dh);
            writer.Write(Unknown_Eh);
            writer.Write(Unknown_10h);
            writer.Write(Unknown_14h);
            writer.WriteBlock(KeyframeProp0);
            writer.WriteBlock(KeyframeProp1);
            writer.WriteBlock(KeyframeProp2);
            writer.WriteBlock(KeyframeProp3);
            writer.Write(Unknown_258h);
            writer.Write(Unknown_25Ch);
            writer.WriteBlock(KeyframeProps);
            writer.Write(Unknown_270h);
            writer.Write(Unknown_278h);
        }
        public virtual void WriteXml(StringBuilder sb, int indent)
        {
            YptXml.ValueTag(sb, indent, "Type", DomainType.ToString());
            YptXml.ValueTag(sb, indent, "Unknown10", YptXml.UintString(Unknown_10h));
            YptXml.ValueTag(sb, indent, "Unknown258", FloatUtil.ToString(Unknown_258h));
            if (KeyframeProp0 != null)
            {
                YptXml.OpenTag(sb, indent, "KeyframeProperty0");
                KeyframeProp0.WriteXml(sb, indent + 1);
                YptXml.CloseTag(sb, indent, "KeyframeProperty0");
            }
            if (KeyframeProp1 != null)
            {
                YptXml.OpenTag(sb, indent, "KeyframeProperty1");
                KeyframeProp1.WriteXml(sb, indent + 1);
                YptXml.CloseTag(sb, indent, "KeyframeProperty1");
            }
            if (KeyframeProp2 != null)
            {
                YptXml.OpenTag(sb, indent, "KeyframeProperty2");
                KeyframeProp2.WriteXml(sb, indent + 1);
                YptXml.CloseTag(sb, indent, "KeyframeProperty2");
            }
            if (KeyframeProp3 != null)
            {
                YptXml.OpenTag(sb, indent, "KeyframeProperty3");
                KeyframeProp3.WriteXml(sb, indent + 1);
                YptXml.CloseTag(sb, indent, "KeyframeProperty3");
            }
        }
        public virtual void ReadXml(XmlNode node)
        {
            DomainType = Xml.GetEnumValue<ParticleDomainType>(Xml.GetChildStringAttribute(node, "Type"));
            Unknown_10h = Xml.GetChildUIntAttribute(node, "Unknown10");
            Unknown_258h = Xml.GetChildFloatAttribute(node, "Unknown258");

            KeyframeProp0 = new ParticleKeyframeProp();
            var pnode0 = node.SelectSingleNode("KeyframeProperty0");
            if (pnode0 != null)
            {
                KeyframeProp0.ReadXml(pnode0);
            }

            KeyframeProp1 = new ParticleKeyframeProp();
            var pnode1 = node.SelectSingleNode("KeyframeProperty1");
            if (pnode1 != null)
            {
                KeyframeProp1.ReadXml(pnode1);
            }

            KeyframeProp2 = new ParticleKeyframeProp();
            var pnode2 = node.SelectSingleNode("KeyframeProperty2");
            if (pnode2 != null)
            {
                KeyframeProp2.ReadXml(pnode2);
            }

            KeyframeProp3 = new ParticleKeyframeProp();
            var pnode3 = node.SelectSingleNode("KeyframeProperty3");
            if (pnode3 != null)
            {
                KeyframeProp3.ReadXml(pnode3);
            }

            KeyframeProps = new ResourcePointerList64<ParticleKeyframeProp>();
            KeyframeProps.data_items = new[] { KeyframeProp0, KeyframeProp1, KeyframeProp3, KeyframeProp2, null, null, null, null, null, null, null, null, null, null, null, null };

        }
        public static void WriteXmlNode(ParticleDomain d, StringBuilder sb, int indent, string name)
        {
            if (d != null)
            {
                YptXml.OpenTag(sb, indent, name);
                d.WriteXml(sb, indent + 1);
                YptXml.CloseTag(sb, indent, name);
            }
        }
        public static ParticleDomain ReadXmlNode(XmlNode node)
        {
            if (node != null)
            {
                var typestr = Xml.GetChildStringAttribute(node, "Type");
                var type = Xml.GetEnumValue<ParticleDomainType>(typestr);
                var s = Create(type);
                if (s != null)
                {
                    s.ReadXml(node);
                }
                return s;
            }
            return null;
        }

        public override Tuple<long, IResourceBlock>[] GetParts()
        {
            KeyframeProps.ManualCountOverride = true;
            KeyframeProps.ManualReferenceOverride = true;
            KeyframeProps.EntriesCount = 4;
            KeyframeProps.EntriesCapacity = 16;

            return new Tuple<long, IResourceBlock>[] {
                new Tuple<long, IResourceBlock>(24, KeyframeProp0),
                new Tuple<long, IResourceBlock>(168, KeyframeProp1),
                new Tuple<long, IResourceBlock>(312, KeyframeProp2),
                new Tuple<long, IResourceBlock>(456, KeyframeProp3),
                new Tuple<long, IResourceBlock>(0x260, KeyframeProps)
            };
        }

        public IResourceSystemBlock GetType(ResourceDataReader reader, params object[] parameters)
        {
            reader.Position += 12;
            var type = (ParticleDomainType)reader.ReadByte();
            reader.Position -= 13;
            return Create(type);
        }
        public static ParticleDomain Create(ParticleDomainType type)
        {
            switch (type)
            {
                case ParticleDomainType.Box: return new ParticleDomainBox();
                case ParticleDomainType.Sphere: return new ParticleDomainSphere();
                case ParticleDomainType.Cylinder: return new ParticleDomainCylinder();
                case ParticleDomainType.Attractor: return new ParticleDomainAttractor();
                default: return null;// throw new Exception("Unknown domain type");
            }
        }

        public override string ToString()
        {
            return "Domain: " + DomainType.ToString();
        }

    }

    [TC(typeof(EXP))] public class ParticleDomainBox : ParticleDomain
    {
        // ptxDomainBox
    }
    
    [TC(typeof(EXP))] public class ParticleDomainSphere : ParticleDomain
    {
        // ptxDomainSphere 
    }

    [TC(typeof(EXP))] public class ParticleDomainCylinder : ParticleDomain
    {
        // ptxDomainCylinder   
    }

    [TC(typeof(EXP))] public class ParticleDomainAttractor : ParticleDomain
    {
        // ptxDomainAttractor
    }














    public enum ParticleBehaviourType : uint
    {
        Age = 0xF5B33BAA,
        Acceleration = 0xD63D9F1B,
        Velocity = 0x6C0719BC,
        Rotation = 0x1EE64552,
        Size = 0x38B60240,
        Dampening = 0x052B1293,
        MatrixWeight = 0x64E5D702,
        Collision = 0x928A1C45,
        AnimateTexture = 0xECA84C1E,
        Colour = 0x164AEA72,
        Sprite = 0x68FA73F5,
        Wind = 0x38B63978,
        Light = 0x0544C710,
        Model = 0x6232E25A,
        Decal = 0x8F3B6036,
        ZCull = 0xA35C721F,
        Noise = 0xB77FED19,
        Attractor = 0x25AC9437,
        Trail = 0xC57377F8,
        FogVolume = 0xA05DA63E,
        River = 0xD4594BEF,
        DecalPool = 0xA2D6DC3F,
        Liquid = 0xDF229542
    }

    [TC(typeof(EXP))] public class ParticleBehaviour : ResourceSystemBlock, IResourceXXSystemBlock, IMetaXmlItem
    {
        // ptxBehaviour
        public override long BlockLength => 0x30;

        // structure data
        public uint VFT { get; set; }
        public uint Unknown_4h = 1; // 0x00000001
        public ParticleBehaviourType Type { get; set; }
        public uint Unknown_Ch; // 0x00000000
        public ResourcePointerList64<ParticleKeyframeProp> KeyframeProps { get; set; }
        public ulong Unknown_20h; // 0x0000000000000000
        public ulong Unknown_28h; // 0x0000000000000000

        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            // read structure data
            VFT = reader.ReadUInt32();
            Unknown_4h = reader.ReadUInt32();
            Type = (ParticleBehaviourType)reader.ReadUInt32();
            Unknown_Ch = reader.ReadUInt32();
            KeyframeProps = reader.ReadBlock<ResourcePointerList64<ParticleKeyframeProp>>();
            Unknown_20h = reader.ReadUInt64();
            Unknown_28h = reader.ReadUInt64();

            KeyframeProps.ManualCountOverride = true; //incase re-saving again
            KeyframeProps.ManualReferenceOverride = true;

            //if (Unknown_4h != 1)
            //{ }//no hit
            //if (Unknown_Ch != 0)
            //{ }//no hit
            //if (Unknown_20h != 0)
            //{ }//no hit
            //if (Unknown_28h != 0)
            //{ }//no hit
            //if ((KeyframeProps?.EntriesCount > 0) && (KeyframeProps.EntriesCapacity != 16))
            //{ }//no hit
        }
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            // write structure data
            writer.Write(VFT);
            writer.Write(Unknown_4h);
            writer.Write((uint)Type);
            writer.Write(Unknown_Ch);
            writer.WriteBlock(KeyframeProps);
            writer.Write(Unknown_20h);
            writer.Write(Unknown_28h);
        }
        public virtual void WriteXml(StringBuilder sb, int indent)
        {
            YptXml.ValueTag(sb, indent, "Type", Type.ToString());
        }
        public virtual void ReadXml(XmlNode node)
        {
            Type = Xml.GetEnumValue<ParticleBehaviourType>(Xml.GetChildStringAttribute(node, "Type"));

            KeyframeProps = new ResourcePointerList64<ParticleKeyframeProp>();//incase subclass doesn't create it
        }
        public static void WriteXmlNode(ParticleBehaviour b, StringBuilder sb, int indent, string name)
        {
            if (b != null)
            {
                YptXml.OpenTag(sb, indent, name);
                b.WriteXml(sb, indent + 1);
                YptXml.CloseTag(sb, indent, name);
            }
        }
        public static ParticleBehaviour ReadXmlNode(XmlNode node)
        {
            if (node != null)
            {
                var typestr = Xml.GetChildStringAttribute(node, "Type");
                var type = Xml.GetEnumValue<ParticleBehaviourType>(typestr);
                var s = Create(type);
                if (s != null)
                {
                    s.ReadXml(node);
                }
                return s;
            }
            return null;
        }

        public IResourceSystemBlock GetType(ResourceDataReader reader, params object[] parameters)
        {

            reader.Position += 8;
            ParticleBehaviourType type = (ParticleBehaviourType)reader.ReadUInt32();
            reader.Position -= 12;

            return Create(type);
        }
        public static ParticleBehaviour Create(ParticleBehaviourType type)
        {
            switch (type)
            {
                case ParticleBehaviourType.Age: return new ParticleBehaviourAge();
                case ParticleBehaviourType.Acceleration: return new ParticleBehaviourAcceleration();
                case ParticleBehaviourType.Velocity: return new ParticleBehaviourVelocity();
                case ParticleBehaviourType.Rotation: return new ParticleBehaviourRotation();
                case ParticleBehaviourType.Size: return new ParticleBehaviourSize();
                case ParticleBehaviourType.Dampening: return new ParticleBehaviourDampening();
                case ParticleBehaviourType.MatrixWeight: return new ParticleBehaviourMatrixWeight();
                case ParticleBehaviourType.Collision: return new ParticleBehaviourCollision();
                case ParticleBehaviourType.AnimateTexture: return new ParticleBehaviourAnimateTexture();
                case ParticleBehaviourType.Colour: return new ParticleBehaviourColour();
                case ParticleBehaviourType.Sprite: return new ParticleBehaviourSprite();
                case ParticleBehaviourType.Wind: return new ParticleBehaviourWind();
                case ParticleBehaviourType.Light: return new ParticleBehaviourLight();
                case ParticleBehaviourType.Model: return new ParticleBehaviourModel();
                case ParticleBehaviourType.Decal: return new ParticleBehaviourDecal();
                case ParticleBehaviourType.ZCull: return new ParticleBehaviourZCull();
                case ParticleBehaviourType.Noise: return new ParticleBehaviourNoise();
                case ParticleBehaviourType.Attractor: return new ParticleBehaviourAttractor();
                case ParticleBehaviourType.Trail: return new ParticleBehaviourTrail();
                case ParticleBehaviourType.FogVolume: return new ParticleBehaviourFogVolume();
                case ParticleBehaviourType.River: return new ParticleBehaviourRiver();
                case ParticleBehaviourType.DecalPool: return new ParticleBehaviourDecalPool();
                case ParticleBehaviourType.Liquid: return new ParticleBehaviourLiquid();
                default: return null;// throw new Exception("Unknown behaviour type");
            }
        }

        public override string ToString()
        {
            return "Behaviour: " + Type.ToString();
        }


        public void CreateKeyframeProps(params ParticleKeyframeProp[] props)
        {
            var plist = props.ToList();
            if (plist.Count > 0)
            {
                for (int i = plist.Count; i < 16; i++)
                {
                    plist.Add(null);
                }
            }

            KeyframeProps = new ResourcePointerList64<ParticleKeyframeProp>();
            KeyframeProps.data_items = plist.ToArray();
            KeyframeProps.ManualCountOverride = true;
            KeyframeProps.ManualReferenceOverride = true;
            KeyframeProps.EntriesCount = (ushort)(props?.Length ?? 0);
            KeyframeProps.EntriesCapacity = (ushort)((plist.Count > 0) ? 16 : 0);

        }

        public override Tuple<long, IResourceBlock>[] GetParts()
        {
            return new Tuple<long, IResourceBlock>[] {
                new Tuple<long, IResourceBlock>(0x10, KeyframeProps)
            };
        }

    }

    [TC(typeof(EXP))] public class ParticleBehaviourAge : ParticleBehaviour
    {
        // ptxu_Age
        public override long BlockLength => 0x30;


        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            base.Read(reader, parameters);

            // read structure data
        }
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            base.Write(writer, parameters);
        }
    }

    [TC(typeof(EXP))]
    public class ParticleBehaviourAcceleration : ParticleBehaviour
    {
        // ptxu_Acceleration
        public override long BlockLength => 0x170;

        // structure data
        public ParticleKeyframeProp XYZMinKFP { get; set; }
        public ParticleKeyframeProp XYZMaxKFP { get; set; }
        public ulong unused00 { get; set; }
        public int ReferenceSpace { get; set; }
        public byte IsAffectedByZoom { get; set; }
        public byte EnableGravity { get; set; }
        public short padding00 { get; set; }
        public ulong padding01 { get; set; }
        public ulong padding02 { get; set; }

        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            base.Read(reader, parameters);

            // read structure data
            XYZMinKFP = reader.ReadBlock<ParticleKeyframeProp>();
            XYZMaxKFP = reader.ReadBlock<ParticleKeyframeProp>();
            unused00 = reader.ReadUInt64();
            ReferenceSpace = reader.ReadInt32();
            IsAffectedByZoom = reader.ReadByte();
            EnableGravity = reader.ReadByte();
            padding00 = reader.ReadInt16();
            padding01 = reader.ReadUInt64();
            padding02 = reader.ReadUInt64();
        }
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            base.Write(writer, parameters);

            // write structure data
            writer.WriteBlock(XYZMinKFP);
            writer.WriteBlock(XYZMaxKFP);
            writer.Write(unused00);
            writer.Write(ReferenceSpace);
            writer.Write(IsAffectedByZoom);
            writer.Write(EnableGravity);
            writer.Write(padding00);
            writer.Write(padding01);
            writer.Write(padding02);
        }
        public override void WriteXml(StringBuilder sb, int indent)
        {
            base.WriteXml(sb, indent);
            YptXml.ValueTag(sb, indent, "ReferenceSpace", ReferenceSpace.ToString());
            YptXml.ValueTag(sb, indent, "IsAffectedByZoom", IsAffectedByZoom.ToString());
            YptXml.ValueTag(sb, indent, "EnableGravity", EnableGravity.ToString());
            if (XYZMinKFP != null)
            {
                YptXml.OpenTag(sb, indent, "XYZMinKFP");
                XYZMinKFP.WriteXml(sb, indent + 1);
                YptXml.CloseTag(sb, indent, "XYZMinKFP");
            }
            if (XYZMaxKFP != null)
            {
                YptXml.OpenTag(sb, indent, "XYZMaxKFP");
                XYZMaxKFP.WriteXml(sb, indent + 1);
                YptXml.CloseTag(sb, indent, "XYZMaxKFP");
            }
        }
        public override void ReadXml(XmlNode node)
        {
            base.ReadXml(node);
            ReferenceSpace = Xml.GetChildIntAttribute(node, "ReferenceSpace");
            IsAffectedByZoom = (byte)Xml.GetChildUIntAttribute(node, "IsAffectedByZoom");
            EnableGravity = (byte)Xml.GetChildUIntAttribute(node, "EnableGravity");

            XYZMinKFP = new ParticleKeyframeProp();
            var pnode0 = node.SelectSingleNode("XYZMinKFP");
            if (pnode0 != null)
            {
                XYZMinKFP.ReadXml(pnode0);
            }

            XYZMaxKFP = new ParticleKeyframeProp();
            var pnode1 = node.SelectSingleNode("XYZMaxKFP");
            if (pnode1 != null)
            {
                XYZMaxKFP.ReadXml(pnode1);
            }

            CreateKeyframeProps(XYZMinKFP, XYZMaxKFP);
        }

        public override Tuple<long, IResourceBlock>[] GetParts()
        {
            return new Tuple<long, IResourceBlock>[] {
                new Tuple<long, IResourceBlock>(0x10, KeyframeProps),
                new Tuple<long, IResourceBlock>(48, XYZMinKFP),
                new Tuple<long, IResourceBlock>(192, XYZMaxKFP)
            };
        }
    }

    [TC(typeof(EXP))] public class ParticleBehaviourVelocity : ParticleBehaviour
    {
        // ptxu_Velocity
        public override long BlockLength => 0x30;


        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            base.Read(reader, parameters);

            // read structure data
        }
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            base.Write(writer, parameters);
        }
    }

    [TC(typeof(EXP))]
    public class ParticleBehaviourRotation : ParticleBehaviour
    {
        // ptxu_Rotation
        public override long BlockLength => 0x280;

        // structure data
        public ParticleKeyframeProp InitialAngleMinKFP { get; set; }
        public ParticleKeyframeProp InitialAngleMaxKFP { get; set; }
        public ParticleKeyframeProp AngleMinKFP { get; set; }
        public ParticleKeyframeProp AngleMaxKFP { get; set; }
        public int InitRotationMode { get; set; }
        public int UpdateRotationMode { get; set; }
        public byte AccumulateAngle { get; set; }
        public byte RotateAngleAxes { get; set; }
        public byte RotateInitAngleAxes { get; set; }
        public byte padding00 { get; set; }
        public float SpeedFadeThreshold { get; set; }

        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            base.Read(reader, parameters);

            // read structure data
            InitialAngleMinKFP = reader.ReadBlock<ParticleKeyframeProp>();
            InitialAngleMaxKFP = reader.ReadBlock<ParticleKeyframeProp>();
            AngleMinKFP = reader.ReadBlock<ParticleKeyframeProp>();
            AngleMaxKFP = reader.ReadBlock<ParticleKeyframeProp>();
            InitRotationMode = reader.ReadInt32();
            UpdateRotationMode = reader.ReadInt32();
            AccumulateAngle = reader.ReadByte();
            RotateAngleAxes = reader.ReadByte();
            RotateInitAngleAxes = reader.ReadByte();
            padding00 = reader.ReadByte();
            SpeedFadeThreshold = reader.ReadSingle();
        }
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            base.Write(writer, parameters);

            // write structure data
            writer.WriteBlock(InitialAngleMinKFP);
            writer.WriteBlock(InitialAngleMaxKFP);
            writer.WriteBlock(AngleMinKFP);
            writer.WriteBlock(AngleMaxKFP);
            writer.Write(InitRotationMode);
            writer.Write(UpdateRotationMode);
            writer.Write(AccumulateAngle);
            writer.Write(RotateAngleAxes);
            writer.Write(RotateInitAngleAxes);
            writer.Write(padding00);
            writer.Write(SpeedFadeThreshold);
        }
        public override void WriteXml(StringBuilder sb, int indent)
        {
            base.WriteXml(sb, indent);
            YptXml.ValueTag(sb, indent, "InitRotationMode", InitRotationMode.ToString());
            YptXml.ValueTag(sb, indent, "UpdateRotationMode", UpdateRotationMode.ToString());
            YptXml.ValueTag(sb, indent, "AccumulateAngle", AccumulateAngle.ToString());
            YptXml.ValueTag(sb, indent, "RotateAngleAxes", RotateAngleAxes.ToString());
            YptXml.ValueTag(sb, indent, "RotateInitAngleAxes", RotateInitAngleAxes.ToString());
            YptXml.ValueTag(sb, indent, "SpeedFadeThreshold", FloatUtil.ToString(SpeedFadeThreshold));
            if (InitialAngleMinKFP != null)
            {
                YptXml.OpenTag(sb, indent, "InitialAngleMinKFP");
                InitialAngleMinKFP.WriteXml(sb, indent + 1);
                YptXml.CloseTag(sb, indent, "InitialAngleMinKFP");
            }
            if (InitialAngleMaxKFP != null)
            {
                YptXml.OpenTag(sb, indent, "InitialAngleMaxKFP");
                InitialAngleMaxKFP.WriteXml(sb, indent + 1);
                YptXml.CloseTag(sb, indent, "InitialAngleMaxKFP");
            }
            if (AngleMinKFP != null)
            {
                YptXml.OpenTag(sb, indent, "AngleMinKFP");
                AngleMinKFP.WriteXml(sb, indent + 1);
                YptXml.CloseTag(sb, indent, "AngleMinKFP");
            }
            if (AngleMaxKFP != null)
            {
                YptXml.OpenTag(sb, indent, "AngleMaxKFP");
                AngleMaxKFP.WriteXml(sb, indent + 1);
                YptXml.CloseTag(sb, indent, "AngleMaxKFP");
            }
        }
        public override void ReadXml(XmlNode node)
        {
            base.ReadXml(node);
            InitRotationMode = Xml.GetChildIntAttribute(node, "InitRotationMode");
            UpdateRotationMode = Xml.GetChildIntAttribute(node, "UpdateRotationMode");
            AccumulateAngle = (byte)Xml.GetChildUIntAttribute(node, "AccumulateAngle");
            RotateAngleAxes = (byte)Xml.GetChildUIntAttribute(node, "RotateAngleAxes");
            RotateInitAngleAxes = (byte)Xml.GetChildUIntAttribute(node, "RotateInitAngleAxes");
            SpeedFadeThreshold = Xml.GetChildFloatAttribute(node, "SpeedFadeThreshold");

            InitialAngleMinKFP = new ParticleKeyframeProp();
            var pnode0 = node.SelectSingleNode("InitialAngleMinKFP");
            if (pnode0 != null)
            {
                InitialAngleMinKFP.ReadXml(pnode0);
            }

            InitialAngleMaxKFP = new ParticleKeyframeProp();
            var pnode1 = node.SelectSingleNode("InitialAngleMaxKFP");
            if (pnode1 != null)
            {
                InitialAngleMaxKFP.ReadXml(pnode1);
            }

            AngleMinKFP = new ParticleKeyframeProp();
            var pnode2 = node.SelectSingleNode("AngleMinKFP");
            if (pnode2 != null)
            {
                AngleMinKFP.ReadXml(pnode2);
            }

            AngleMaxKFP = new ParticleKeyframeProp();
            var pnode3 = node.SelectSingleNode("AngleMaxKFP");
            if (pnode3 != null)
            {
                AngleMaxKFP.ReadXml(pnode3);
            }

            CreateKeyframeProps(InitialAngleMinKFP, InitialAngleMaxKFP, AngleMinKFP, AngleMaxKFP);
        }

        public override Tuple<long, IResourceBlock>[] GetParts()
        {
            return new Tuple<long, IResourceBlock>[] {
                new Tuple<long, IResourceBlock>(16, KeyframeProps),
                new Tuple<long, IResourceBlock>(48, InitialAngleMinKFP),
                new Tuple<long, IResourceBlock>(192, InitialAngleMaxKFP),
                new Tuple<long, IResourceBlock>(336, AngleMinKFP),
                new Tuple<long, IResourceBlock>(480, AngleMaxKFP)
            };
        }
    }

    [TC(typeof(EXP))] public class ParticleBehaviourSize : ParticleBehaviour
    {
        // ptxu_Size
        public override long BlockLength => 0x280;

        // structure data
        public ParticleKeyframeProp WhdMinKFP { get; set; }
        public ParticleKeyframeProp WhdMaxKFP { get; set; }
        public ParticleKeyframeProp TblrScalarKFP { get; set; }
        public ParticleKeyframeProp TblrVelScalarKFP { get; set; }
        public int KeyframeMode { get; set; }
        public byte IsProportional { get; set; }
        public byte padding00 { get; set; }
        public short padding01 { get; set; }
        public ulong padding02 { get; set; }

        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            base.Read(reader, parameters);

            // read structure data
            WhdMinKFP = reader.ReadBlock<ParticleKeyframeProp>();
            WhdMaxKFP = reader.ReadBlock<ParticleKeyframeProp>();
            TblrScalarKFP = reader.ReadBlock<ParticleKeyframeProp>();
            TblrVelScalarKFP = reader.ReadBlock<ParticleKeyframeProp>();
            KeyframeMode = reader.ReadInt32();
            IsProportional = reader.ReadByte();
            padding00 = reader.ReadByte();
            padding01 = reader.ReadInt16();
            padding02 = reader.ReadUInt64();
        }
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            base.Write(writer, parameters);

            // write structure data
            writer.WriteBlock(WhdMinKFP);
            writer.WriteBlock(WhdMaxKFP);
            writer.WriteBlock(TblrScalarKFP);
            writer.WriteBlock(TblrVelScalarKFP);
            writer.Write(KeyframeMode);
            writer.Write(IsProportional);
            writer.Write(padding00);
            writer.Write(padding01);
            writer.Write(padding02);
        }
        public override void WriteXml(StringBuilder sb, int indent)
        {
            base.WriteXml(sb, indent);
            YptXml.ValueTag(sb, indent, "KeyframeMode", KeyframeMode.ToString());
            YptXml.ValueTag(sb, indent, "IsProportional", IsProportional.ToString());
            if (WhdMinKFP != null)
            {
                YptXml.OpenTag(sb, indent, "WhdMinKFP");
                WhdMinKFP.WriteXml(sb, indent + 1);
                YptXml.CloseTag(sb, indent, "WhdMinKFP");
            }
            if (WhdMaxKFP != null)
            {
                YptXml.OpenTag(sb, indent, "WhdMaxKFP");
                WhdMaxKFP.WriteXml(sb, indent + 1);
                YptXml.CloseTag(sb, indent, "WhdMaxKFP");
            }
            if (TblrScalarKFP != null)
            {
                YptXml.OpenTag(sb, indent, "TblrScalarKFP");
                TblrScalarKFP.WriteXml(sb, indent + 1);
                YptXml.CloseTag(sb, indent, "TblrScalarKFP");
            }
            if (TblrVelScalarKFP != null)
            {
                YptXml.OpenTag(sb, indent, "TblrVelScalarKFP");
                TblrVelScalarKFP.WriteXml(sb, indent + 1);
                YptXml.CloseTag(sb, indent, "TblrVelScalarKFP");
            }
        }
        public override void ReadXml(XmlNode node)
        {
            base.ReadXml(node);
            KeyframeMode = Xml.GetChildIntAttribute(node, "KeyframeMode");
            IsProportional = (byte)Xml.GetChildUIntAttribute(node, "KeyframeMode");

            WhdMinKFP = new ParticleKeyframeProp();
            var pnode0 = node.SelectSingleNode("WhdMinKFP");
            if (pnode0 != null)
            {
                WhdMinKFP.ReadXml(pnode0);
            }

            WhdMaxKFP = new ParticleKeyframeProp();
            var pnode1 = node.SelectSingleNode("WhdMaxKFP");
            if (pnode1 != null)
            {
                WhdMaxKFP.ReadXml(pnode1);
            }

            TblrScalarKFP = new ParticleKeyframeProp();
            var pnode2 = node.SelectSingleNode("TblrScalarKFP");
            if (pnode2 != null)
            {
                TblrScalarKFP.ReadXml(pnode2);
            }

            TblrVelScalarKFP = new ParticleKeyframeProp();
            var pnode3 = node.SelectSingleNode("TblrVelScalarKFP");
            if (pnode3 != null)
            {
                TblrVelScalarKFP.ReadXml(pnode3);
            }

            CreateKeyframeProps(WhdMinKFP, WhdMaxKFP, TblrScalarKFP, TblrVelScalarKFP);
        }

        public override Tuple<long, IResourceBlock>[] GetParts()
        {
            return new Tuple<long, IResourceBlock>[] {
                new Tuple<long, IResourceBlock>(16, KeyframeProps),
                new Tuple<long, IResourceBlock>(48, WhdMinKFP),
                new Tuple<long, IResourceBlock>(192, WhdMaxKFP),
                new Tuple<long, IResourceBlock>(336, TblrScalarKFP),
                new Tuple<long, IResourceBlock>(480, TblrVelScalarKFP)
            };
        }
    }

    [TC(typeof(EXP))]
    public class ParticleBehaviourDampening : ParticleBehaviour
    {
        // ptxu_Dampening
        public override long BlockLength => 0x170;

        // structure data
        public ParticleKeyframeProp XYZMinKFP { get; set; }
        public ParticleKeyframeProp XYZMaxKFP { get; set; }
        public ulong unused00 { get; set; }
        public int ReferenceSpace { get; set; }
        public byte EnableAirResistance { get; set; }
        public byte padding00 { get; set; }
        public short padding01 { get; set; }
        public ulong padding02 { get; set; }
        public ulong padding03 { get; set; }

        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            base.Read(reader, parameters);

            // read structure data
            XYZMinKFP = reader.ReadBlock<ParticleKeyframeProp>();
            XYZMaxKFP = reader.ReadBlock<ParticleKeyframeProp>();
            unused00 = reader.ReadUInt64();
            ReferenceSpace = reader.ReadInt32();
            EnableAirResistance = reader.ReadByte();
            padding00 = reader.ReadByte();
            padding01 = reader.ReadInt16();
            padding02 = reader.ReadUInt64();
            padding03 = reader.ReadUInt64();
        }
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            base.Write(writer, parameters);

            // write structure data
            writer.WriteBlock(XYZMinKFP);
            writer.WriteBlock(XYZMaxKFP);
            writer.Write(unused00);
            writer.Write(ReferenceSpace);
            writer.Write(EnableAirResistance);
            writer.Write(padding00);
            writer.Write(padding01);
            writer.Write(padding02);
            writer.Write(padding03);
        }
        public override void WriteXml(StringBuilder sb, int indent)
        {
            base.WriteXml(sb, indent);
            YptXml.ValueTag(sb, indent, "ReferenceSpace", ReferenceSpace.ToString());
            YptXml.ValueTag(sb, indent, "EnableAirResistance", EnableAirResistance.ToString());
            if (XYZMinKFP != null)
            {
                YptXml.OpenTag(sb, indent, "XYZMinKFP");
                XYZMinKFP.WriteXml(sb, indent + 1);
                YptXml.CloseTag(sb, indent, "XYZMinKFP");
            }
            if (XYZMaxKFP != null)
            {
                YptXml.OpenTag(sb, indent, "XYZMaxKFP");
                XYZMaxKFP.WriteXml(sb, indent + 1);
                YptXml.CloseTag(sb, indent, "XYZMaxKFP");
            }
        }
        public override void ReadXml(XmlNode node)
        {
            base.ReadXml(node);
            ReferenceSpace = Xml.GetChildIntAttribute(node, "ReferenceSpace");
            EnableAirResistance = (byte)Xml.GetChildIntAttribute(node, "EnableAirResistance");

            XYZMinKFP = new ParticleKeyframeProp();
            var pnode0 = node.SelectSingleNode("XYZMinKFP");
            if (pnode0 != null)
            {
                XYZMinKFP.ReadXml(pnode0);
            }

            XYZMaxKFP = new ParticleKeyframeProp();
            var pnode1 = node.SelectSingleNode("XYZMaxKFP");
            if (pnode1 != null)
            {
                XYZMaxKFP.ReadXml(pnode1);
            }

            CreateKeyframeProps(XYZMinKFP, XYZMaxKFP);
        }

        public override Tuple<long, IResourceBlock>[] GetParts()
        {
            return new Tuple<long, IResourceBlock>[] {
                new Tuple<long, IResourceBlock>(16, KeyframeProps),
                new Tuple<long, IResourceBlock>(48, XYZMinKFP),
                new Tuple<long, IResourceBlock>(192, XYZMaxKFP)
            };
        }
    }
    [TC(typeof(EXP))]
    public class ParticleBehaviourMatrixWeight : ParticleBehaviour
    {
        // ptxu_MatrixWeight
        public override long BlockLength => 0xD0;

        // structure data
        public ParticleKeyframeProp mtxWeightKFP { get; set; }
        public int ReferenceSpace { get; set; }
        public uint padding00 { get; set; }
        public ulong padding01 { get; set; }

        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            base.Read(reader, parameters);

            // read structure data
            mtxWeightKFP = reader.ReadBlock<ParticleKeyframeProp>();
            ReferenceSpace = reader.ReadInt32();
            padding00 = reader.ReadUInt32();
            padding01 = reader.ReadUInt64();
        }
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            base.Write(writer, parameters);

            // write structure data
            writer.WriteBlock(mtxWeightKFP);
            writer.Write(ReferenceSpace);
            writer.Write(padding00);
            writer.Write(padding01);
        }
        public override void WriteXml(StringBuilder sb, int indent)
        {
            base.WriteXml(sb, indent);
            YptXml.ValueTag(sb, indent, "ReferenceSpace", ReferenceSpace.ToString());
            if (mtxWeightKFP != null)
            {
                YptXml.OpenTag(sb, indent, "mtxWeightKFP");
                mtxWeightKFP.WriteXml(sb, indent + 1);
                YptXml.CloseTag(sb, indent, "mtxWeightKFP");
            }
        }
        public override void ReadXml(XmlNode node)
        {
            base.ReadXml(node);
            ReferenceSpace = Xml.GetChildIntAttribute(node, "ReferenceSpace");

            mtxWeightKFP = new ParticleKeyframeProp();
            var pnode0 = node.SelectSingleNode("mtxWeightKFP");
            if (pnode0 != null)
            {
                mtxWeightKFP.ReadXml(pnode0);
            }

            CreateKeyframeProps(mtxWeightKFP);
        }

        public override Tuple<long, IResourceBlock>[] GetParts()
        {
            return new Tuple<long, IResourceBlock>[] {
                new Tuple<long, IResourceBlock>(16, KeyframeProps),
                new Tuple<long, IResourceBlock>(48, mtxWeightKFP)
            };
        }
    }

    [TC(typeof(EXP))] public class ParticleBehaviourCollision : ParticleBehaviour
    {
        // ptxu_Collision
        public override long BlockLength => 0x170;

        // structure data
        public ParticleKeyframeProp BouncinessKFP { get; set; }
        public ParticleKeyframeProp BounceDirVarKFP { get; set; }
        public float RadiusMult { get; set; }
        public float RestSpeed { get; set; }
        public int CollisionChance { get; set; }
        public int KillChance { get; set; }
        public byte DebugDraw { get; set; }
        public byte padding00 { get; set; }
        public short padding01 { get; set; }
        public float OverrideMinRadius { get; set; }
        public ulong padding02 { get; set; }

        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            base.Read(reader, parameters);

            // read structure data
            BouncinessKFP = reader.ReadBlock<ParticleKeyframeProp>();
            BounceDirVarKFP = reader.ReadBlock<ParticleKeyframeProp>();
            RadiusMult = reader.ReadSingle();
            RestSpeed = reader.ReadSingle();
            CollisionChance = reader.ReadInt32();
            KillChance = reader.ReadInt32();
            DebugDraw = reader.ReadByte();
            padding00 =  reader.ReadByte();
            padding01 = reader.ReadInt16();
            OverrideMinRadius = reader.ReadSingle();
            padding02 = reader.ReadUInt64();
        }
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            base.Write(writer, parameters);

            // write structure data
            writer.WriteBlock(BouncinessKFP);
            writer.WriteBlock(BounceDirVarKFP);
            writer.Write(RadiusMult);
            writer.Write(RestSpeed);
            writer.Write(CollisionChance);
            writer.Write(KillChance);
            writer.Write(DebugDraw);
            writer.Write(padding00);
            writer.Write(padding01);
            writer.Write(OverrideMinRadius);
            writer.Write(padding02);
        }
        public override void WriteXml(StringBuilder sb, int indent)
        {
            base.WriteXml(sb, indent);
            YptXml.ValueTag(sb, indent, "RadiusMult", FloatUtil.ToString(RadiusMult));
            YptXml.ValueTag(sb, indent, "RestSpeed", FloatUtil.ToString(RestSpeed));
            YptXml.ValueTag(sb, indent, "CollisionChance", CollisionChance.ToString());
            YptXml.ValueTag(sb, indent, "KillChance", KillChance.ToString());
            YptXml.ValueTag(sb, indent, "OverrideMinRadius", FloatUtil.ToString(OverrideMinRadius));
            if (BouncinessKFP != null)
            {
                YptXml.OpenTag(sb, indent, "BouncinessKFP");
                BouncinessKFP.WriteXml(sb, indent + 1);
                YptXml.CloseTag(sb, indent, "BouncinessKFP");
            }
            if (BounceDirVarKFP != null)
            {
                YptXml.OpenTag(sb, indent, "BounceDirVarKFP");
                BounceDirVarKFP.WriteXml(sb, indent + 1);
                YptXml.CloseTag(sb, indent, "BounceDirVarKFP");
            }
        }
        public override void ReadXml(XmlNode node)
        {
            base.ReadXml(node);
            RadiusMult = Xml.GetChildFloatAttribute(node, "RadiusMult");
            RestSpeed = Xml.GetChildFloatAttribute(node, "RestSpeed");
            CollisionChance = Xml.GetChildIntAttribute(node, "CollisionChance");
            KillChance = Xml.GetChildIntAttribute(node, "KillChance");
            OverrideMinRadius = Xml.GetChildFloatAttribute(node, "OverrideMinRadius");

            BouncinessKFP = new ParticleKeyframeProp();
            var pnode0 = node.SelectSingleNode("BouncinessKFP");
            if (pnode0 != null)
            {
                BouncinessKFP.ReadXml(pnode0);
            }

            BounceDirVarKFP = new ParticleKeyframeProp();
            var pnode1 = node.SelectSingleNode("BounceDirVarKFP");
            if (pnode1 != null)
            {
                BounceDirVarKFP.ReadXml(pnode1);
            }

            CreateKeyframeProps(BouncinessKFP, BounceDirVarKFP);
        }

        public override Tuple<long, IResourceBlock>[] GetParts()
        {
            return new Tuple<long, IResourceBlock>[] {
                new Tuple<long, IResourceBlock>(16, KeyframeProps),
                new Tuple<long, IResourceBlock>(48, BouncinessKFP),
                new Tuple<long, IResourceBlock>(192, BounceDirVarKFP)
            };
        }
    }

    [TC(typeof(EXP))]
    public class ParticleBehaviourAnimateTexture : ParticleBehaviour
    {
        // ptxu_AnimateTexture
        public override long BlockLength => 0xD0;

        // structure data
        public ParticleKeyframeProp AnimRateKFP { get; set; }
        public int KeyframeMode { get; set; }
        public int LastFrameID { get; set; }
        public int LoopMode { get; set; }
        public byte IsRandomised { get; set; }
        public byte IsScaledOverParticleLife { get; set; }
        public byte IsHeldOnLastFrame { get; set; }
        public byte DoFrameBlending { get; set; }

        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            base.Read(reader, parameters);

            // read structure data
            AnimRateKFP = reader.ReadBlock<ParticleKeyframeProp>();
            KeyframeMode = reader.ReadInt32();
            LastFrameID = reader.ReadInt32();
            LoopMode = reader.ReadInt32();
            IsRandomised = reader.ReadByte();
            IsScaledOverParticleLife = reader.ReadByte();
            IsHeldOnLastFrame = reader.ReadByte();
            DoFrameBlending = reader.ReadByte();
        }
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            base.Write(writer, parameters);

            // write structure data
            writer.WriteBlock(AnimRateKFP);
            writer.Write(KeyframeMode);
            writer.Write(LastFrameID);
            writer.Write(LoopMode);
            writer.Write(IsRandomised);
            writer.Write(IsScaledOverParticleLife);
            writer.Write(IsHeldOnLastFrame);
            writer.Write(DoFrameBlending);
        }
        public override void WriteXml(StringBuilder sb, int indent)
        {
            base.WriteXml(sb, indent);
            YptXml.ValueTag(sb, indent, "KeyframeMode", KeyframeMode.ToString());
            YptXml.ValueTag(sb, indent, "LastFrameID", LastFrameID.ToString());
            YptXml.ValueTag(sb, indent, "LoopMode", LoopMode.ToString());
            YptXml.ValueTag(sb, indent, "IsRandomised", IsRandomised.ToString());
            YptXml.ValueTag(sb, indent, "IsScaledOverParticleLife", IsScaledOverParticleLife.ToString());
            YptXml.ValueTag(sb, indent, "IsHeldOnLastFrame", IsHeldOnLastFrame.ToString());
            YptXml.ValueTag(sb, indent, "DoFrameBlending", DoFrameBlending.ToString());
            if (AnimRateKFP != null)
            {
                YptXml.OpenTag(sb, indent, "AnimRateKFP");
                AnimRateKFP.WriteXml(sb, indent + 1);
                YptXml.CloseTag(sb, indent, "AnimRateKFP");
            }
        }
        public override void ReadXml(XmlNode node)
        {
            base.ReadXml(node);
            KeyframeMode = Xml.GetChildIntAttribute(node, "KeyframeMode");
            LastFrameID = Xml.GetChildIntAttribute(node, "LastFrameID");
            LoopMode = Xml.GetChildIntAttribute(node, "LoopMode");
            IsRandomised = (byte)Xml.GetChildUIntAttribute(node, "IsRandomised");
            IsScaledOverParticleLife = (byte)Xml.GetChildUIntAttribute(node, "IsScaledOverParticleLife");
            IsHeldOnLastFrame = (byte)Xml.GetChildUIntAttribute(node, "IsHeldOnLastFrame");
            DoFrameBlending = (byte)Xml.GetChildUIntAttribute(node, "DoFrameBlending");

            AnimRateKFP = new ParticleKeyframeProp();
            var pnode0 = node.SelectSingleNode("AnimRateKFP");
            if (pnode0 != null)
            {
                AnimRateKFP.ReadXml(pnode0);
            }

            CreateKeyframeProps(AnimRateKFP);
        }

        public override Tuple<long, IResourceBlock>[] GetParts()
        {
            return new Tuple<long, IResourceBlock>[] {
                new Tuple<long, IResourceBlock>(16, KeyframeProps),
                new Tuple<long, IResourceBlock>(48, AnimRateKFP)
            };
        }
    }

    [TC(typeof(EXP))]
    public class ParticleBehaviourColour : ParticleBehaviour
    {
        // ptxu_Colour
        public override long BlockLength => 0x1F0;

        // structure data
        public ParticleKeyframeProp RGBAMinKFP { get; set; }
        public ParticleKeyframeProp RGBAMaxKFP { get; set; }
        public ParticleKeyframeProp EmissiveIntensityKFP { get; set; }
        public int KeyframeMode { get; set; }
        public byte RGBAMaxEnable { get; set; }
        public byte RGBAProportional { get; set; }
        public byte RGBCanTint { get; set; }
        public byte padding00 { get; set; }
        public ulong padding01 { get; set; }

        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            base.Read(reader, parameters);

            // read structure data
            RGBAMinKFP = reader.ReadBlock<ParticleKeyframeProp>();
            RGBAMaxKFP = reader.ReadBlock<ParticleKeyframeProp>();
            EmissiveIntensityKFP = reader.ReadBlock<ParticleKeyframeProp>();
            KeyframeMode = reader.ReadInt32();
            RGBAMaxEnable = reader.ReadByte();
            RGBAProportional = reader.ReadByte();
            RGBCanTint = reader.ReadByte();
            padding00 = reader.ReadByte();
            padding01 = reader.ReadUInt64();
        }
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            base.Write(writer, parameters);

            // write structure data
            writer.WriteBlock(RGBAMinKFP);
            writer.WriteBlock(RGBAMaxKFP);
            writer.WriteBlock(EmissiveIntensityKFP);
            writer.Write(KeyframeMode);
            writer.Write(RGBAMaxEnable);
            writer.Write(RGBAProportional);
            writer.Write(RGBCanTint);
            writer.Write(padding00);
            writer.Write(padding01);
        }
        public override void WriteXml(StringBuilder sb, int indent)
        {
            base.WriteXml(sb, indent);
            YptXml.ValueTag(sb, indent, "KeyframeMode", KeyframeMode.ToString());
            YptXml.ValueTag(sb, indent, "RGBAMaxEnable", RGBAMaxEnable.ToString());
            YptXml.ValueTag(sb, indent, "RGBAProportional", RGBAProportional.ToString());
            YptXml.ValueTag(sb, indent, "RGBCanTint", RGBCanTint.ToString());
            if (RGBAMinKFP != null)
            {
                YptXml.OpenTag(sb, indent, "RGBAMinKFP");
                RGBAMinKFP.WriteXml(sb, indent + 1);
                YptXml.CloseTag(sb, indent, "RGBAMinKFP");
            }
            if (RGBAMaxKFP != null)
            {
                YptXml.OpenTag(sb, indent, "RGBAMaxKFP");
                RGBAMaxKFP.WriteXml(sb, indent + 1);
                YptXml.CloseTag(sb, indent, "RGBAMaxKFP");
            }
            if (EmissiveIntensityKFP != null)
            {
                YptXml.OpenTag(sb, indent, "EmissiveIntensityKFP");
                EmissiveIntensityKFP.WriteXml(sb, indent + 1);
                YptXml.CloseTag(sb, indent, "EmissiveIntensityKFP");
            }
        }
        public override void ReadXml(XmlNode node)
        {
            base.ReadXml(node);
            KeyframeMode = Xml.GetChildIntAttribute(node, "KeyframeMode");
            RGBAMaxEnable = (byte)Xml.GetChildUIntAttribute(node, "RGBAMaxEnable");
            RGBAProportional = (byte)Xml.GetChildUIntAttribute(node, "RGBAProportional");
            RGBCanTint = (byte)Xml.GetChildUIntAttribute(node, "RGBCanTint");

            RGBAMinKFP = new ParticleKeyframeProp();
            var pnode0 = node.SelectSingleNode("RGBAMinKFP");
            if (pnode0 != null)
            {
                RGBAMinKFP.ReadXml(pnode0);
            }

            RGBAMaxKFP = new ParticleKeyframeProp();
            var pnode1 = node.SelectSingleNode("RGBAMaxKFP");
            if (pnode1 != null)
            {
                RGBAMaxKFP.ReadXml(pnode1);
            }

            EmissiveIntensityKFP = new ParticleKeyframeProp();
            var pnode2 = node.SelectSingleNode("EmissiveIntensityKFP");
            if (pnode2 != null)
            {
                EmissiveIntensityKFP.ReadXml(pnode2);
            }

            CreateKeyframeProps(RGBAMinKFP, RGBAMaxKFP, EmissiveIntensityKFP);
        }

        public override Tuple<long, IResourceBlock>[] GetParts()
        {
            return new Tuple<long, IResourceBlock>[] {
                new Tuple<long, IResourceBlock>(16, KeyframeProps),
                new Tuple<long, IResourceBlock>(48, RGBAMinKFP),
                new Tuple<long, IResourceBlock>(192, RGBAMaxKFP),
                new Tuple<long, IResourceBlock>(336, EmissiveIntensityKFP)
            };
        }
    }

    [TC(typeof(EXP))] public class ParticleBehaviourSprite : ParticleBehaviour
    {
        // ptxd_Sprite
        public override long BlockLength => 0x70;

        // structure data
        public Vector3 AlignAxis { get; set; }
        public uint padding00 { get; set; }
        public int AlignmentMode { get; set; }
        public float FlipChanceU { get; set; }
        public float FlipChanceV { get; set; }
        public float NearClipDist { get; set; }
        public float FarClipDist { get; set; }
        public float ProjectionDepth { get; set; }
        public float ShadowCastIntensity { get; set; }
        public byte IsScreenSpace { get; set; }
        public byte IsHighRes { get; set; }
        public byte NearClip { get; set; }
        public byte FarClip { get; set; }
        public byte UVClip { get; set; }
        public byte DisableDraw { get; set; }
        public short padding01 { get; set; }
        public uint padding02 { get; set; }
        public ulong padding03 { get; set; }

        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            base.Read(reader, parameters);

            // read structure data
            AlignAxis = new Vector3(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
            padding00 = reader.ReadUInt32();
            AlignmentMode = reader.ReadInt32();
            FlipChanceU = reader.ReadSingle();
            FlipChanceV = reader.ReadSingle();
            NearClipDist = reader.ReadSingle();
            FarClipDist = reader.ReadSingle();
            ProjectionDepth = reader.ReadSingle();
            ShadowCastIntensity = reader.ReadSingle();
            IsScreenSpace = reader.ReadByte();
            IsHighRes = reader.ReadByte();
            NearClip = reader.ReadByte();
            FarClip = reader.ReadByte();
            UVClip = reader.ReadByte();
            DisableDraw = reader.ReadByte();
            padding01 = reader.ReadInt16();
            padding02 = reader.ReadUInt32();
            padding03 = reader.ReadUInt64();
        }
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            base.Write(writer, parameters);

            // write structure data
            writer.Write(AlignAxis.X);
            writer.Write(AlignAxis.Y);
            writer.Write(AlignAxis.Z);
            writer.Write(padding00);
            writer.Write(AlignmentMode);
            writer.Write(FlipChanceU);
            writer.Write(FlipChanceV);
            writer.Write(NearClipDist);
            writer.Write(FarClipDist);
            writer.Write(ProjectionDepth);
            writer.Write(ShadowCastIntensity);
            writer.Write(IsScreenSpace);
            writer.Write(IsHighRes);
            writer.Write(NearClip);
            writer.Write(FarClip);
            writer.Write(UVClip);
            writer.Write(DisableDraw);
            writer.Write(padding01);
            writer.Write(padding02);
            writer.Write(padding03);
        }
        public override void WriteXml(StringBuilder sb, int indent)
        {
            base.WriteXml(sb, indent);
            RelXml.SelfClosingTag(sb, indent, "AlignAxis " + FloatUtil.GetVector3XmlString(AlignAxis));
            YptXml.ValueTag(sb, indent, "AlignmentMode", AlignmentMode.ToString());
            YptXml.ValueTag(sb, indent, "FlipChanceU", FloatUtil.ToString(FlipChanceU));
            YptXml.ValueTag(sb, indent, "FlipChanceV", FloatUtil.ToString(FlipChanceV));
            YptXml.ValueTag(sb, indent, "NearClipDist", FloatUtil.ToString(NearClipDist));
            YptXml.ValueTag(sb, indent, "FarClipDist", FloatUtil.ToString(FarClipDist));
            YptXml.ValueTag(sb, indent, "ProjectionDepth", FloatUtil.ToString(ProjectionDepth));
            YptXml.ValueTag(sb, indent, "ShadowCastIntensity", FloatUtil.ToString(ShadowCastIntensity));
            YptXml.ValueTag(sb, indent, "IsScreenSpace", IsScreenSpace.ToString());
            YptXml.ValueTag(sb, indent, "IsHighRes", IsHighRes.ToString());
            YptXml.ValueTag(sb, indent, "NearClip", NearClip.ToString());
            YptXml.ValueTag(sb, indent, "FarClip", FarClip.ToString());
            YptXml.ValueTag(sb, indent, "UVClip", UVClip.ToString());
            YptXml.ValueTag(sb, indent, "DisableDraw", DisableDraw.ToString());
        }
        public override void ReadXml(XmlNode node)
        {
            base.ReadXml(node);
            AlignAxis = Xml.GetChildVector3Attributes(node, "AlignAxis");;
            AlignmentMode = Xml.GetChildIntAttribute(node, "AlignmentMode");
            FlipChanceU = Xml.GetChildFloatAttribute(node, "FlipChanceU");
            FlipChanceV = Xml.GetChildFloatAttribute(node, "FlipChanceV");
            NearClipDist = Xml.GetChildFloatAttribute(node, "NearClipDist");
            FarClipDist = Xml.GetChildFloatAttribute(node, "FarClipDist");
            ProjectionDepth = Xml.GetChildFloatAttribute(node, "ProjectionDepth");
            ShadowCastIntensity = Xml.GetChildFloatAttribute(node, "ShadowCastIntensity");
            IsScreenSpace = (byte)Xml.GetChildUIntAttribute(node, "IsScreenSpace");
            IsHighRes = (byte)Xml.GetChildUIntAttribute(node, "IsHighRes");
            NearClip = (byte)Xml.GetChildUIntAttribute(node, "NearClip");
            FarClip = (byte)Xml.GetChildUIntAttribute(node, "FarClip");
            UVClip = (byte)Xml.GetChildUIntAttribute(node, "UVClip");
            DisableDraw = (byte)Xml.GetChildUIntAttribute(node, "DisableDraw");
        }
    }

    [TC(typeof(EXP))]
    public class ParticleBehaviourWind : ParticleBehaviour
    {
        // ptxu_Wind
        public override long BlockLength => 0xF0;

        // structure data
        public ParticleKeyframeProp InfluenceKFP { get; set; }
        public ulong unused00 { get; set; }
        public ulong unused01 { get; set; }
        public float HighLodRange { get; set; }
        public float LowLodRange { get; set; }
        public int HighLodDisturbanceMode { get; set; }
        public int LodLodDisturbanceMode { get; set; }
        public byte IgnoreMtxWeight { get; set; }
        public byte padding00 { get; set; }
        public short padding01 { get; set; }
        public uint padding02 { get; set; }
        public ulong padding03 { get; set; }

        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            base.Read(reader, parameters);

            // read structure data
            InfluenceKFP = reader.ReadBlock<ParticleKeyframeProp>();
            unused00 = reader.ReadUInt64();
            unused01 = reader.ReadUInt64();
            HighLodRange = reader.ReadSingle();
            LowLodRange = reader.ReadSingle();
            HighLodDisturbanceMode = reader.ReadInt32();
            LodLodDisturbanceMode = reader.ReadInt32();
            IgnoreMtxWeight = reader.ReadByte();
            padding00 = reader.ReadByte();
            padding01 = reader.ReadInt16();
            padding02 = reader.ReadUInt32();
            padding03 = reader.ReadUInt64();
        }
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            base.Write(writer, parameters);

            // write structure data
            writer.WriteBlock(InfluenceKFP);
            writer.Write(unused00);
            writer.Write(unused01);
            writer.Write(HighLodRange);
            writer.Write(LowLodRange);
            writer.Write(HighLodDisturbanceMode);
            writer.Write(LodLodDisturbanceMode);
            writer.Write(IgnoreMtxWeight);
            writer.Write(padding00);
            writer.Write(padding01);
            writer.Write(padding02);
            writer.Write(padding03);
        }
        public override void WriteXml(StringBuilder sb, int indent)
        {
            base.WriteXml(sb, indent);
            YptXml.ValueTag(sb, indent, "HighLodRange", FloatUtil.ToString(HighLodRange));
            YptXml.ValueTag(sb, indent, "LowLodRange", FloatUtil.ToString(LowLodRange));
            YptXml.ValueTag(sb, indent, "HighLodDisturbanceMode", HighLodDisturbanceMode.ToString());
            YptXml.ValueTag(sb, indent, "LodLodDisturbanceMode", LodLodDisturbanceMode.ToString());
            YptXml.ValueTag(sb, indent, "IgnoreMtxWeight", IgnoreMtxWeight.ToString());
            if (InfluenceKFP != null)
            {
                YptXml.OpenTag(sb, indent, "InfluenceKFP");
                InfluenceKFP.WriteXml(sb, indent + 1);
                YptXml.CloseTag(sb, indent, "InfluenceKFP");
            }
        }
        public override void ReadXml(XmlNode node)
        {
            base.ReadXml(node);
            HighLodRange = Xml.GetChildFloatAttribute(node, "HighLodRange");
            LowLodRange = Xml.GetChildFloatAttribute(node, "LowLodRange");
            HighLodDisturbanceMode = Xml.GetChildIntAttribute(node, "HighLodDisturbanceMode");
            LodLodDisturbanceMode = Xml.GetChildIntAttribute(node, "LodLodDisturbanceMode");
            IgnoreMtxWeight = (byte)Xml.GetChildUIntAttribute(node, "IgnoreMtxWeight");
            InfluenceKFP = new ParticleKeyframeProp();
            var pnode0 = node.SelectSingleNode("InfluenceKFP");
            if (pnode0 != null)
            {
                InfluenceKFP.ReadXml(pnode0);
            }

            CreateKeyframeProps(InfluenceKFP);
        }

        public override Tuple<long, IResourceBlock>[] GetParts()
        {
            return new Tuple<long, IResourceBlock>[] {
                new Tuple<long, IResourceBlock>(16, KeyframeProps),
                new Tuple<long, IResourceBlock>(48, InfluenceKFP)
            };
        }
    }

    [TC(typeof(EXP))]
    public class ParticleBehaviourLight : ParticleBehaviour
    {
        // ptxu_Light
        public override long BlockLength => 0x550;

        // structure data
        public ParticleKeyframeProp RGBMinKFP { get; set; }
        public ParticleKeyframeProp RGBMaxKFP { get; set; }
        public ParticleKeyframeProp IntensityKFP { get; set; }
        public ParticleKeyframeProp RangeKFP { get; set; }
        public ParticleKeyframeProp CoronaRGBMinKFP { get; set; }
        public ParticleKeyframeProp CoronaRGBMaxKFP { get; set; }
        public ParticleKeyframeProp CoronaIntensityKFP { get; set; }
        public ParticleKeyframeProp CoronaSizeKFP { get; set; }
        public ParticleKeyframeProp CoronaFlareKFP { get; set; }
        public float CoronaZBias { get; set; }
        public byte CoronaUseLightColour { get; set; }
        public byte ColourFromParticle { get; set; }
        public byte ColourPerFrame { get; set; }
        public byte IntensityPerFrame { get; set; }
        public byte RangePerFrame { get; set; }
        public byte CastsShadows { get; set; }
        public byte CoronaNotInReflection { get; set; }
        public byte CoronaOnlyInReflection { get; set; }
        public int LightType { get; set; }

        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            base.Read(reader, parameters);

            // read structure data
            RGBMinKFP = reader.ReadBlock<ParticleKeyframeProp>();
            RGBMaxKFP = reader.ReadBlock<ParticleKeyframeProp>();
            IntensityKFP = reader.ReadBlock<ParticleKeyframeProp>();
            RangeKFP = reader.ReadBlock<ParticleKeyframeProp>();
            CoronaRGBMinKFP = reader.ReadBlock<ParticleKeyframeProp>();
            CoronaRGBMaxKFP = reader.ReadBlock<ParticleKeyframeProp>();
            CoronaIntensityKFP = reader.ReadBlock<ParticleKeyframeProp>();
            CoronaSizeKFP = reader.ReadBlock<ParticleKeyframeProp>();
            CoronaFlareKFP = reader.ReadBlock<ParticleKeyframeProp>();
            CoronaZBias = reader.ReadSingle();
            CoronaUseLightColour = reader.ReadByte();
            ColourFromParticle = reader.ReadByte();
            ColourPerFrame = reader.ReadByte();
            IntensityPerFrame = reader.ReadByte();
            RangePerFrame = reader.ReadByte();
            CastsShadows = reader.ReadByte();
            CoronaNotInReflection = reader.ReadByte();
            CoronaOnlyInReflection = reader.ReadByte();
            LightType = reader.ReadInt32();
        }
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            base.Write(writer, parameters);

            // write structure data
            writer.WriteBlock(RGBMinKFP);
            writer.WriteBlock(RGBMaxKFP);
            writer.WriteBlock(IntensityKFP);
            writer.WriteBlock(RangeKFP);
            writer.WriteBlock(CoronaRGBMinKFP);
            writer.WriteBlock(CoronaRGBMaxKFP);
            writer.WriteBlock(CoronaIntensityKFP);
            writer.WriteBlock(CoronaSizeKFP);
            writer.WriteBlock(CoronaFlareKFP);
            writer.Write(CoronaZBias);
            writer.Write(CoronaUseLightColour);
            writer.Write(ColourFromParticle);
            writer.Write(ColourPerFrame);
            writer.Write(IntensityPerFrame);
            writer.Write(RangePerFrame);
            writer.Write(CastsShadows);
            writer.Write(CoronaNotInReflection);
            writer.Write(CoronaOnlyInReflection);
            writer.Write(LightType);
        }
        public override void WriteXml(StringBuilder sb, int indent)
        {
            base.WriteXml(sb, indent);
            YptXml.ValueTag(sb, indent, "CoronaZBias", FloatUtil.ToString(CoronaZBias));
            YptXml.ValueTag(sb, indent, "CoronaUseLightColour", CoronaUseLightColour.ToString());
            YptXml.ValueTag(sb, indent, "CoronaUseLightColour", ColourFromParticle.ToString());
            YptXml.ValueTag(sb, indent, "CoronaUseLightColour", ColourPerFrame.ToString());
            YptXml.ValueTag(sb, indent, "CoronaUseLightColour", IntensityPerFrame.ToString());
            YptXml.ValueTag(sb, indent, "RangePerFrame", RangePerFrame.ToString());
            YptXml.ValueTag(sb, indent, "CastsShadows", CastsShadows.ToString());
            YptXml.ValueTag(sb, indent, "CoronaNotInReflection", CoronaNotInReflection.ToString());
            YptXml.ValueTag(sb, indent, "CoronaOnlyInReflection", CoronaOnlyInReflection.ToString());
            YptXml.ValueTag(sb, indent, "LightType", LightType.ToString());
            if (RGBMinKFP != null)
            {
                YptXml.OpenTag(sb, indent, "RGBMinKFP");
                RGBMinKFP.WriteXml(sb, indent + 1);
                YptXml.CloseTag(sb, indent, "RGBMinKFP");
            }
            if (RGBMaxKFP != null)
            {
                YptXml.OpenTag(sb, indent, "RGBMaxKFP");
                RGBMaxKFP.WriteXml(sb, indent + 1);
                YptXml.CloseTag(sb, indent, "RGBMaxKFP");
            }
            if (IntensityKFP != null)
            {
                YptXml.OpenTag(sb, indent, "IntensityKFP");
                IntensityKFP.WriteXml(sb, indent + 1);
                YptXml.CloseTag(sb, indent, "IntensityKFP");
            }
            if (RangeKFP != null)
            {
                YptXml.OpenTag(sb, indent, "RangeKFP");
                RangeKFP.WriteXml(sb, indent + 1);
                YptXml.CloseTag(sb, indent, "RangeKFP");
            }
            if (CoronaRGBMinKFP != null)
            {
                YptXml.OpenTag(sb, indent, "CoronaRGBMinKFP");
                CoronaRGBMinKFP.WriteXml(sb, indent + 1);
                YptXml.CloseTag(sb, indent, "CoronaRGBMinKFP");
            }
            if (CoronaRGBMaxKFP != null)
            {
                YptXml.OpenTag(sb, indent, "CoronaRGBMaxKFP");
                CoronaRGBMaxKFP.WriteXml(sb, indent + 1);
                YptXml.CloseTag(sb, indent, "CoronaRGBMaxKFP");
            }
            if (CoronaIntensityKFP != null)
            {
                YptXml.OpenTag(sb, indent, "CoronaIntensityKFP");
                CoronaIntensityKFP.WriteXml(sb, indent + 1);
                YptXml.CloseTag(sb, indent, "CoronaIntensityKFP");
            }
            if (CoronaSizeKFP != null)
            {
                YptXml.OpenTag(sb, indent, "CoronaSizeKFP");
                CoronaSizeKFP.WriteXml(sb, indent + 1);
                YptXml.CloseTag(sb, indent, "CoronaSizeKFP");
            }
            if (CoronaFlareKFP != null)
            {
                YptXml.OpenTag(sb, indent, "CoronaFlareKFP");
                CoronaFlareKFP.WriteXml(sb, indent + 1);
                YptXml.CloseTag(sb, indent, "CoronaFlareKFP");
            }
        }
        public override void ReadXml(XmlNode node)
        {
            base.ReadXml(node);
            CoronaZBias = Xml.GetChildFloatAttribute(node, "CoronaZBias");
            CoronaUseLightColour = (byte)Xml.GetChildUIntAttribute(node, "CoronaUseLightColour");
            ColourFromParticle = (byte)Xml.GetChildUIntAttribute(node, "ColourFromParticle");
            ColourPerFrame = (byte)Xml.GetChildUIntAttribute(node, "ColourPerFrame");
            IntensityPerFrame = (byte)Xml.GetChildUIntAttribute(node, "IntensityPerFrame");
            RangePerFrame = (byte)Xml.GetChildUIntAttribute(node, "RangePerFrame");
            CastsShadows = (byte)Xml.GetChildUIntAttribute(node, "CastsShadows");
            CoronaNotInReflection = (byte)Xml.GetChildUIntAttribute(node, "CoronaNotInReflection");
            CoronaOnlyInReflection = (byte)Xml.GetChildUIntAttribute(node, "CoronaOnlyInReflection");
            LightType = Xml.GetChildIntAttribute(node, "LightType");

            RGBMinKFP = new ParticleKeyframeProp();
            var pnode0 = node.SelectSingleNode("RGBMinKFP");
            if (pnode0 != null)
            {
                RGBMinKFP.ReadXml(pnode0);
            }

            RGBMaxKFP = new ParticleKeyframeProp();
            var pnode1 = node.SelectSingleNode("RGBMaxKFP");
            if (pnode1 != null)
            {
                RGBMaxKFP.ReadXml(pnode1);
            }

            IntensityKFP = new ParticleKeyframeProp();
            var pnode2 = node.SelectSingleNode("IntensityKFP");
            if (pnode2 != null)
            {
                IntensityKFP.ReadXml(pnode2);
            }

            RangeKFP = new ParticleKeyframeProp();
            var pnode3 = node.SelectSingleNode("RangeKFP");
            if (pnode3 != null)
            {
                RangeKFP.ReadXml(pnode3);
            }

            CoronaRGBMinKFP = new ParticleKeyframeProp();
            var pnode4 = node.SelectSingleNode("CoronaRGBMinKFP");
            if (pnode4 != null)
            {
                CoronaRGBMinKFP.ReadXml(pnode4);
            }

            CoronaRGBMaxKFP = new ParticleKeyframeProp();
            var pnode5 = node.SelectSingleNode("CoronaRGBMaxKFP");
            if (pnode5 != null)
            {
                CoronaRGBMaxKFP.ReadXml(pnode5);
            }

            CoronaIntensityKFP = new ParticleKeyframeProp();
            var pnode6 = node.SelectSingleNode("CoronaIntensityKFP");
            if (pnode6 != null)
            {
                CoronaIntensityKFP.ReadXml(pnode6);
            }

            CoronaSizeKFP = new ParticleKeyframeProp();
            var pnode7 = node.SelectSingleNode("CoronaSizeKFP");
            if (pnode7 != null)
            {
                CoronaSizeKFP.ReadXml(pnode7);
            }

            CoronaFlareKFP = new ParticleKeyframeProp();
            var pnode8 = node.SelectSingleNode("CoronaFlareKFP");
            if (pnode8 != null)
            {
                CoronaFlareKFP.ReadXml(pnode8);
            }

            CreateKeyframeProps(RGBMinKFP, RGBMaxKFP, IntensityKFP, RangeKFP, CoronaRGBMinKFP, CoronaRGBMaxKFP, CoronaIntensityKFP, CoronaSizeKFP, CoronaFlareKFP);
        }

        public override Tuple<long, IResourceBlock>[] GetParts()
        {
            return new Tuple<long, IResourceBlock>[] {
                new Tuple<long, IResourceBlock>(16, KeyframeProps),
                new Tuple<long, IResourceBlock>(48, RGBMinKFP),
                new Tuple<long, IResourceBlock>(192, RGBMaxKFP),
                new Tuple<long, IResourceBlock>(336, IntensityKFP),
                new Tuple<long, IResourceBlock>(480, RangeKFP),
                new Tuple<long, IResourceBlock>(624, CoronaRGBMinKFP),
                new Tuple<long, IResourceBlock>(768, CoronaRGBMaxKFP),
                new Tuple<long, IResourceBlock>(912, CoronaIntensityKFP),
                new Tuple<long, IResourceBlock>(1056, CoronaSizeKFP),
                new Tuple<long, IResourceBlock>(1200, CoronaFlareKFP)
            };
        }
    }

    [TC(typeof(EXP))]
    public class ParticleBehaviourModel : ParticleBehaviour
    {
        // ptxd_Model
        public override long BlockLength => 0x40;

        // structure data
        public uint ColourControlShaderID { get; set; }
        public float CameraShrink { get; set; }
        public float ShadowCastIntensity { get; set; }
        public byte DisableDraw { get; set; }
        public byte padding00 { get; set; }
        public short padding01 { get; set; }

        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            base.Read(reader, parameters);

            // read structure data
            ColourControlShaderID = reader.ReadUInt32();
            CameraShrink = reader.ReadSingle();
            ShadowCastIntensity = reader.ReadSingle();
            DisableDraw = reader.ReadByte();
            padding00 = reader.ReadByte();
            padding01 = reader.ReadInt16();
        }
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            base.Write(writer, parameters);

            // write structure data
            writer.Write(ColourControlShaderID);
            writer.Write(CameraShrink);
            writer.Write(ShadowCastIntensity);
            writer.Write(DisableDraw);
            writer.Write(padding00);
            writer.Write(padding01);
        }
        public override void WriteXml(StringBuilder sb, int indent)
        {
            base.WriteXml(sb, indent);
            YptXml.ValueTag(sb, indent, "CameraShrink", FloatUtil.ToString(CameraShrink));
            YptXml.ValueTag(sb, indent, "ShadowCastIntensity", FloatUtil.ToString(ShadowCastIntensity));
            YptXml.ValueTag(sb, indent, "DisableDraw", DisableDraw.ToString());
        }
        public override void ReadXml(XmlNode node)
        {
            base.ReadXml(node);
            CameraShrink = Xml.GetChildFloatAttribute(node, "CameraShrink");
            ShadowCastIntensity = Xml.GetChildFloatAttribute(node, "ShadowCastIntensity");
            DisableDraw = (byte)Xml.GetChildUIntAttribute(node, "DisableDraw");
        }
    }

    [TC(typeof(EXP))]
    public class ParticleBehaviourDecal : ParticleBehaviour
    {
        // ptxu_Decal
        public override long BlockLength => 0x180;

        // structure data
        public ParticleKeyframeProp DimensionsKFP { get; set; }
        public ParticleKeyframeProp AlphaKFP { get; set; }
        public int DecalID { get; set; }
        public float VelocityThreshold { get; set; }
        public float TotalLife { get; set; }
        public float FadeInTime { get; set; }
        public float UVMultStart { get; set; }
        public float UVMultEnd { get; set; }
        public float UVMultTime { get; set; }
        public float DuplicateRejectDist { get; set; }
        public byte FlipU { get; set; }
        public byte FlipV { get; set; }
        public byte ProportionalSize { get; set; }
        public byte UseComplexCollision { get; set; }
        public float ProjectionDepth { get; set; }
        public float DistanceScale { get; set; }
        public byte IsDirectional { get; set; }
        public byte padding00 { get; set; }
        public short padding01 { get; set; }

        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            base.Read(reader, parameters);

            // read structure data
            DimensionsKFP = reader.ReadBlock<ParticleKeyframeProp>();
            AlphaKFP = reader.ReadBlock<ParticleKeyframeProp>();
            DecalID = reader.ReadInt32();
            VelocityThreshold = reader.ReadSingle();
            TotalLife = reader.ReadSingle();
            FadeInTime = reader.ReadSingle();
            UVMultStart = reader.ReadSingle();
            UVMultEnd = reader.ReadSingle();
            UVMultTime = reader.ReadSingle();
            DuplicateRejectDist = reader.ReadSingle();
            FlipU = reader.ReadByte();
            FlipV = reader.ReadByte();
            ProportionalSize = reader.ReadByte();
            UseComplexCollision = reader.ReadByte();
            ProjectionDepth = reader.ReadSingle();
            DistanceScale = reader.ReadSingle();
            IsDirectional = reader.ReadByte();
            padding00 = reader.ReadByte();
            padding01 = reader.ReadInt16();
        }
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            base.Write(writer, parameters);

            // write structure data
            writer.WriteBlock(DimensionsKFP);
            writer.WriteBlock(AlphaKFP);
            writer.Write(DecalID);
            writer.Write(VelocityThreshold);
            writer.Write(TotalLife);
            writer.Write(FadeInTime);
            writer.Write(UVMultStart);
            writer.Write(UVMultEnd);
            writer.Write(UVMultTime);
            writer.Write(DuplicateRejectDist);
            writer.Write(FlipU);
            writer.Write(FlipV);
            writer.Write(ProportionalSize);
            writer.Write(UseComplexCollision);
            writer.Write(ProjectionDepth);
            writer.Write(DistanceScale);
            writer.Write(IsDirectional);
            writer.Write(padding00);
            writer.Write(padding01);
        }
        public override void WriteXml(StringBuilder sb, int indent)
        {
            base.WriteXml(sb, indent);
            YptXml.ValueTag(sb, indent, "DecalID", DecalID.ToString());
            YptXml.ValueTag(sb, indent, "VelocityThreshold", FloatUtil.ToString(VelocityThreshold));
            YptXml.ValueTag(sb, indent, "TotalLife", FloatUtil.ToString(TotalLife));
            YptXml.ValueTag(sb, indent, "FadeInTime", FloatUtil.ToString(FadeInTime));
            YptXml.ValueTag(sb, indent, "UVMultStart", FloatUtil.ToString(UVMultStart));
            YptXml.ValueTag(sb, indent, "UVMultEnd", FloatUtil.ToString(UVMultEnd));
            YptXml.ValueTag(sb, indent, "UVMultTime", FloatUtil.ToString(UVMultTime));
            YptXml.ValueTag(sb, indent, "DuplicateRejectDist", FloatUtil.ToString(DuplicateRejectDist));
            YptXml.ValueTag(sb, indent, "FlipU", FlipU.ToString());
            YptXml.ValueTag(sb, indent, "FlipV", FlipV.ToString());
            YptXml.ValueTag(sb, indent, "ProportionalSize", ProportionalSize.ToString());
            YptXml.ValueTag(sb, indent, "UseComplexCollision", UseComplexCollision.ToString());
            YptXml.ValueTag(sb, indent, "ProjectionDepth", FloatUtil.ToString(ProjectionDepth));
            YptXml.ValueTag(sb, indent, "DistanceScale", FloatUtil.ToString(DistanceScale));
            if (DimensionsKFP != null)
            {
                YptXml.OpenTag(sb, indent, "DimensionsKFP");
                DimensionsKFP.WriteXml(sb, indent + 1);
                YptXml.CloseTag(sb, indent, "DimensionsKFP");
            }
            if (AlphaKFP != null)
            {
                YptXml.OpenTag(sb, indent, "AlphaKFP");
                AlphaKFP.WriteXml(sb, indent + 1);
                YptXml.CloseTag(sb, indent, "AlphaKFP");
            }
        }
        public override void ReadXml(XmlNode node)
        {
            base.ReadXml(node);
            DecalID = Xml.GetChildIntAttribute(node, "DecalID");
            VelocityThreshold = Xml.GetChildFloatAttribute(node, "VelocityThreshold");
            TotalLife = Xml.GetChildFloatAttribute(node, "TotalLife");
            FadeInTime = Xml.GetChildFloatAttribute(node, "FadeInTime");
            UVMultStart = Xml.GetChildFloatAttribute(node, "UVMultStart");
            UVMultEnd = Xml.GetChildFloatAttribute(node, "UVMultEnd");
            UVMultTime = Xml.GetChildFloatAttribute(node, "UVMultTime");
            DuplicateRejectDist = Xml.GetChildFloatAttribute(node, "DuplicateRejectDist");
            FlipU = (byte)Xml.GetChildUIntAttribute(node, "FlipU");
            FlipV = (byte)Xml.GetChildUIntAttribute(node, "FlipV");
            ProportionalSize = (byte)Xml.GetChildUIntAttribute(node, "ProportionalSize");
            UseComplexCollision = (byte)Xml.GetChildUIntAttribute(node, "UseComplexCollision");
            ProjectionDepth = Xml.GetChildFloatAttribute(node, "ProjectionDepth");
            DistanceScale = Xml.GetChildFloatAttribute(node, "DistanceScale");

            DimensionsKFP = new ParticleKeyframeProp();
            var pnode0 = node.SelectSingleNode("DimensionsKFP");
            if (pnode0 != null)
            {
                DimensionsKFP.ReadXml(pnode0);
            }

            AlphaKFP = new ParticleKeyframeProp();
            var pnode1 = node.SelectSingleNode("AlphaKFP");
            if (pnode1 != null)
            {
                AlphaKFP.ReadXml(pnode1);
            }

            CreateKeyframeProps(DimensionsKFP, AlphaKFP);
        }

        public override Tuple<long, IResourceBlock>[] GetParts()
        {
            return new Tuple<long, IResourceBlock>[] {
                new Tuple<long, IResourceBlock>(16, KeyframeProps),
                new Tuple<long, IResourceBlock>(48, DimensionsKFP),
                new Tuple<long, IResourceBlock>(192, AlphaKFP)
            };
        }
    }

    [TC(typeof(EXP))]
    public class ParticleBehaviourZCull : ParticleBehaviour
    {
        // ptxu_ZCull
        public override long BlockLength => 0x170;

        // structure data
        public ParticleKeyframeProp HeightKFP { get; set; }
        public ParticleKeyframeProp FadeDistKFP { get; set; }
        public ulong unsued00 { get; set; }
        public int CullMode { get; set; }
        public int ReferenceSpace { get; set; }
        public ulong padding00 { get; set; }
        public ulong padding01 { get; set; }

        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            base.Read(reader, parameters);

            // read structure data
            HeightKFP = reader.ReadBlock<ParticleKeyframeProp>();
            FadeDistKFP = reader.ReadBlock<ParticleKeyframeProp>();
            unsued00 = reader.ReadUInt64();
            CullMode = reader.ReadInt32();
            ReferenceSpace = reader.ReadInt32();
            padding00 = reader.ReadUInt64();
            padding01 = reader.ReadUInt64();
        }
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            base.Write(writer, parameters);

            // write structure data
            writer.WriteBlock(HeightKFP);
            writer.WriteBlock(FadeDistKFP);
            writer.Write(unsued00);
            writer.Write(CullMode);
            writer.Write(ReferenceSpace);
            writer.Write(padding00);
            writer.Write(padding01);
        }
        public override void WriteXml(StringBuilder sb, int indent)
        {
            base.WriteXml(sb, indent);
            YptXml.ValueTag(sb, indent, "CullMode", CullMode.ToString());
            YptXml.ValueTag(sb, indent, "ReferenceSpace", ReferenceSpace.ToString());
            if (HeightKFP != null)
            {
                YptXml.OpenTag(sb, indent, "HeightKFP");
                HeightKFP.WriteXml(sb, indent + 1);
                YptXml.CloseTag(sb, indent, "HeightKFP");
            }
            if (FadeDistKFP != null)
            {
                YptXml.OpenTag(sb, indent, "FadeDistKFP");
                FadeDistKFP.WriteXml(sb, indent + 1);
                YptXml.CloseTag(sb, indent, "FadeDistKFP");
            }
        }
        public override void ReadXml(XmlNode node)
        {
            base.ReadXml(node);
            CullMode = Xml.GetChildIntAttribute(node, "CullMode");
            ReferenceSpace = Xml.GetChildIntAttribute(node, "ReferenceSpace");

            HeightKFP = new ParticleKeyframeProp();
            var pnode0 = node.SelectSingleNode("HeightKFP");
            if (pnode0 != null)
            {
                HeightKFP.ReadXml(pnode0);
            }

            FadeDistKFP = new ParticleKeyframeProp();
            var pnode1 = node.SelectSingleNode("FadeDistKFP");
            if (pnode1 != null)
            {
                FadeDistKFP.ReadXml(pnode1);
            }

            CreateKeyframeProps(HeightKFP, FadeDistKFP);
        }

        public override Tuple<long, IResourceBlock>[] GetParts()
        {
            return new Tuple<long, IResourceBlock>[] {
                new Tuple<long, IResourceBlock>(16, KeyframeProps),
                new Tuple<long, IResourceBlock>(48, HeightKFP),
                new Tuple<long, IResourceBlock>(192, FadeDistKFP)
            };
        }
    }

    [TC(typeof(EXP))]
    public class ParticleBehaviourNoise : ParticleBehaviour
    {
        // ptxu_Noise
        public override long BlockLength => 0x280;

        // structure data
        public ParticleKeyframeProp PosNoiseMinKFP { get; set; }
        public ParticleKeyframeProp PosNoiseMaxKFP { get; set; }
        public ParticleKeyframeProp VelNoiseMinKFP { get; set; }
        public ParticleKeyframeProp VelNoiseMaxKFP { get; set; }
        public uint ReferenceSpace { get; set; }
        public byte KeepConstantSpeed { get; set; }
        public byte padding00 { get; set; }
        public short padding01 { get; set; }
        public ulong padding02 { get; set; }

        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            base.Read(reader, parameters);

            // read structure data
            PosNoiseMinKFP = reader.ReadBlock<ParticleKeyframeProp>();
            PosNoiseMaxKFP = reader.ReadBlock<ParticleKeyframeProp>();
            VelNoiseMinKFP = reader.ReadBlock<ParticleKeyframeProp>();
            VelNoiseMaxKFP = reader.ReadBlock<ParticleKeyframeProp>();
            ReferenceSpace = reader.ReadUInt32();
            KeepConstantSpeed = reader.ReadByte();
            padding00 = reader.ReadByte();
            padding01 = reader.ReadInt16();
            padding02 = reader.ReadUInt64();
        }
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            base.Write(writer, parameters);

            // write structure data
            writer.WriteBlock(PosNoiseMinKFP);
            writer.WriteBlock(PosNoiseMaxKFP);
            writer.WriteBlock(VelNoiseMinKFP);
            writer.WriteBlock(VelNoiseMaxKFP);
            writer.Write(ReferenceSpace);
            writer.Write(KeepConstantSpeed);
            writer.Write(padding00);
            writer.Write(padding01);
            writer.Write(padding02);
        }
        public override void WriteXml(StringBuilder sb, int indent)
        {
            base.WriteXml(sb, indent);
            YptXml.ValueTag(sb, indent, "ReferenceSpace", ReferenceSpace.ToString());
            YptXml.ValueTag(sb, indent, "KeepConstantSpeed", KeepConstantSpeed.ToString());
            if (PosNoiseMinKFP != null)
            {
                YptXml.OpenTag(sb, indent, "PosNoiseMinKFP");
                PosNoiseMinKFP.WriteXml(sb, indent + 1);
                YptXml.CloseTag(sb, indent, "PosNoiseMinKFP");
            }
            if (PosNoiseMaxKFP != null)
            {
                YptXml.OpenTag(sb, indent, "PosNoiseMaxKFP");
                PosNoiseMaxKFP.WriteXml(sb, indent + 1);
                YptXml.CloseTag(sb, indent, "PosNoiseMaxKFP");
            }
            if (VelNoiseMinKFP != null)
            {
                YptXml.OpenTag(sb, indent, "VelNoiseMinKFP");
                VelNoiseMinKFP.WriteXml(sb, indent + 1);
                YptXml.CloseTag(sb, indent, "VelNoiseMinKFP");
            }
            if (VelNoiseMaxKFP != null)
            {
                YptXml.OpenTag(sb, indent, "VelNoiseMaxKFP");
                VelNoiseMaxKFP.WriteXml(sb, indent + 1);
                YptXml.CloseTag(sb, indent, "VelNoiseMaxKFP");
            }
        }
        public override void ReadXml(XmlNode node)
        {
            base.ReadXml(node);
            ReferenceSpace = Xml.GetChildUIntAttribute(node, "ReferenceSpace");
            KeepConstantSpeed = (byte)Xml.GetChildUIntAttribute(node, "KeepConstantSpeed");

            PosNoiseMinKFP = new ParticleKeyframeProp();
            var pnode0 = node.SelectSingleNode("PosNoiseMinKFP");
            if (pnode0 != null)
            {
                PosNoiseMinKFP.ReadXml(pnode0);
            }

            PosNoiseMaxKFP = new ParticleKeyframeProp();
            var pnode1 = node.SelectSingleNode("PosNoiseMaxKFP");
            if (pnode1 != null)
            {
                PosNoiseMaxKFP.ReadXml(pnode1);
            }

            VelNoiseMinKFP = new ParticleKeyframeProp();
            var pnode2 = node.SelectSingleNode("VelNoiseMinKFP");
            if (pnode2 != null)
            {
                VelNoiseMinKFP.ReadXml(pnode2);
            }

            VelNoiseMaxKFP = new ParticleKeyframeProp();
            var pnode3 = node.SelectSingleNode("VelNoiseMaxKFP");
            if (pnode3 != null)
            {
                VelNoiseMaxKFP.ReadXml(pnode3);
            }

            CreateKeyframeProps(PosNoiseMinKFP, PosNoiseMaxKFP, VelNoiseMinKFP, VelNoiseMaxKFP);
        }

        public override Tuple<long, IResourceBlock>[] GetParts()
        {
            return new Tuple<long, IResourceBlock>[] {
                new Tuple<long, IResourceBlock>(16, KeyframeProps),
                new Tuple<long, IResourceBlock>(48, PosNoiseMinKFP),
                new Tuple<long, IResourceBlock>(192, PosNoiseMaxKFP),
                new Tuple<long, IResourceBlock>(336, VelNoiseMinKFP),
                new Tuple<long, IResourceBlock>(480, VelNoiseMaxKFP)
            };
        }
    }

    [TC(typeof(EXP))]
    public class ParticleBehaviourAttractor : ParticleBehaviour
    {
        // ptxu_Attractor
        public override long BlockLength => 0xC0;

        // structure data
        public ParticleKeyframeProp StrengthKFP { get; set; }

        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            base.Read(reader, parameters);

            // read structure data
            StrengthKFP = reader.ReadBlock<ParticleKeyframeProp>();


        }
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            base.Write(writer, parameters);

            // write structure data
            writer.WriteBlock(StrengthKFP);
        }
        public override void WriteXml(StringBuilder sb, int indent)
        {
            base.WriteXml(sb, indent);
            if (StrengthKFP != null)
            {
                YptXml.OpenTag(sb, indent, "StrengthKFP");
                StrengthKFP.WriteXml(sb, indent + 1);
                YptXml.CloseTag(sb, indent, "StrengthKFP");
            }
        }
        public override void ReadXml(XmlNode node)
        {
            base.ReadXml(node);

            StrengthKFP = new ParticleKeyframeProp();
            var pnode0 = node.SelectSingleNode("StrengthKFP");
            if (pnode0 != null)
            {
                StrengthKFP.ReadXml(pnode0);
            }

            CreateKeyframeProps(StrengthKFP);
        }

        public override Tuple<long, IResourceBlock>[] GetParts()
        {
            return new Tuple<long, IResourceBlock>[] {
                new Tuple<long, IResourceBlock>(16, KeyframeProps),
                new Tuple<long, IResourceBlock>(48, StrengthKFP)
            };
        }
    }

    [TC(typeof(EXP))]
    public class ParticleBehaviourTrail : ParticleBehaviour
    {
        // ptxd_Trail
        public override long BlockLength => 0xF0;

        // structure data
        public ParticleKeyframeProp TexInfoKFP { get; set; }
        public Vector3 AlignAxis { get; set; }
        public uint padding00 { get; set; }
        public int AlignmentMode { get; set; }
        public int TessellationU { get; set; }
        public int TessellationV { get; set; }
        public float SmoothnessX { get; set; }
        public float SmoothnessY { get; set; }
        public float ProjectionDepth { get; set; }
        public float ShadowCastIntensity { get; set; }
        public byte FlipU { get; set; }
        public byte FlipV { get; set; }
        public byte WrapTextureOverParticleLife { get; set; }
        public byte DisableDraw { get; set; }

        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            base.Read(reader, parameters);

            // read structure data
            TexInfoKFP = reader.ReadBlock<ParticleKeyframeProp>();
            AlignAxis = new Vector3(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
            padding00 = reader.ReadUInt32();
            AlignmentMode = reader.ReadInt32();
            TessellationU = reader.ReadInt32();
            TessellationV = reader.ReadInt32();
            SmoothnessX = reader.ReadSingle();
            SmoothnessY = reader.ReadSingle();
            ProjectionDepth = reader.ReadSingle();
            ShadowCastIntensity = reader.ReadSingle();
            FlipU = reader.ReadByte();
            FlipV = reader.ReadByte();
            WrapTextureOverParticleLife = reader.ReadByte();
            DisableDraw = reader.ReadByte();
        }
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            base.Write(writer, parameters);

            // write structure data
            writer.WriteBlock(TexInfoKFP);
            writer.Write(AlignAxis.X);
            writer.Write(AlignAxis.Y);
            writer.Write(AlignAxis.Z);
            writer.Write(padding00);
            writer.Write(AlignmentMode);
            writer.Write(TessellationU);
            writer.Write(TessellationV);
            writer.Write(SmoothnessX);
            writer.Write(SmoothnessY);
            writer.Write(ProjectionDepth);
            writer.Write(ShadowCastIntensity);
            writer.Write(FlipU);
            writer.Write(FlipV);
            writer.Write(WrapTextureOverParticleLife);
            writer.Write(DisableDraw);
        }
        public override void WriteXml(StringBuilder sb, int indent)
        {
            base.WriteXml(sb, indent);
            RelXml.SelfClosingTag(sb, indent, "AlignAxis " + FloatUtil.GetVector3XmlString(AlignAxis));
            YptXml.ValueTag(sb, indent, "AlignmentMode", AlignmentMode.ToString());
            YptXml.ValueTag(sb, indent, "TessellationU", TessellationU.ToString());
            YptXml.ValueTag(sb, indent, "TessellationV", TessellationV.ToString());
            YptXml.ValueTag(sb, indent, "SmoothnessX", FloatUtil.ToString(SmoothnessX));
            YptXml.ValueTag(sb, indent, "SmoothnessY", FloatUtil.ToString(SmoothnessY));
            YptXml.ValueTag(sb, indent, "ProjectionDepth", FloatUtil.ToString(ProjectionDepth));
            YptXml.ValueTag(sb, indent, "ShadowCastIntensity", FloatUtil.ToString(ShadowCastIntensity));
            YptXml.ValueTag(sb, indent, "FlipU", FlipU.ToString());
            YptXml.ValueTag(sb, indent, "FlipV", FlipV.ToString());
            YptXml.ValueTag(sb, indent, "WrapTextureOverParticleLife", WrapTextureOverParticleLife.ToString());
            YptXml.ValueTag(sb, indent, "DisableDraw", DisableDraw.ToString());
            if (TexInfoKFP != null)
            {
                YptXml.OpenTag(sb, indent, "TexInfoKFP");
                TexInfoKFP.WriteXml(sb, indent + 1);
                YptXml.CloseTag(sb, indent, "TexInfoKFP");
            }
        }
        public override void ReadXml(XmlNode node)
        {
            base.ReadXml(node);
            AlignAxis = Xml.GetChildVector3Attributes(node, "AlignAxis");
            AlignmentMode = Xml.GetChildIntAttribute(node, "AlignmentMode");
            TessellationU = Xml.GetChildIntAttribute(node, "TessellationU");
            TessellationV = Xml.GetChildIntAttribute(node, "TessellationV");
            SmoothnessX = Xml.GetChildFloatAttribute(node, "SmoothnessX");
            SmoothnessY = Xml.GetChildFloatAttribute(node, "SmoothnessY");
            ProjectionDepth = Xml.GetChildFloatAttribute(node, "ProjectionDepth");
            ShadowCastIntensity = Xml.GetChildFloatAttribute(node, "ShadowCastIntensity");
            FlipU = (byte)Xml.GetChildUIntAttribute(node, "FlipU");
            FlipV = (byte)Xml.GetChildUIntAttribute(node, "FlipV");
            WrapTextureOverParticleLife = (byte)Xml.GetChildUIntAttribute(node, "WrapTextureOverParticleLife");
            DisableDraw = (byte)Xml.GetChildUIntAttribute(node, "DisableDraw");

            TexInfoKFP = new ParticleKeyframeProp();
            var pnode0 = node.SelectSingleNode("TexInfoKFP");
            if (pnode0 != null)
            {
                TexInfoKFP.ReadXml(pnode0);
            }

            CreateKeyframeProps(TexInfoKFP);
        }

        public override Tuple<long, IResourceBlock>[] GetParts()
        {
            return new Tuple<long, IResourceBlock>[] {
                new Tuple<long, IResourceBlock>(16, KeyframeProps),
                new Tuple<long, IResourceBlock>(48, TexInfoKFP)
            };
        }
    }

    [TC(typeof(EXP))]
    public class ParticleBehaviourFogVolume : ParticleBehaviour
    {
        // ptxu_FogVolume
        public override long BlockLength => 0x430;

        // structure data
        public ParticleKeyframeProp RGBTintMinKFP { get; set; }
        public ParticleKeyframeProp RGBTintMaxKFP { get; set; }
        public ParticleKeyframeProp DensityRangeKFP { get; set; }
        public ParticleKeyframeProp ScaleMinKFP { get; set; }
        public ParticleKeyframeProp ScaleMaxKFP { get; set; }
        public ParticleKeyframeProp RotationMinKFP { get; set; }
        public ParticleKeyframeProp RotationMaxKFP { get; set; }
        public float Falloff { get; set; } // 1.0f, 3.0f
        public float HDRMult { get; set; } // 1.0f
        public int LightingType { get; set; }
        public byte ColourTintFromParticle { get; set; }
        public byte SortWithParticles { get; set; }
        public byte UseGroundFogColour { get; set; }
        public byte UseEffectEvoValues { get; set; }

        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            base.Read(reader, parameters);

            // read structure data
            RGBTintMinKFP = reader.ReadBlock<ParticleKeyframeProp>();
            RGBTintMaxKFP = reader.ReadBlock<ParticleKeyframeProp>();
            DensityRangeKFP = reader.ReadBlock<ParticleKeyframeProp>();
            ScaleMinKFP = reader.ReadBlock<ParticleKeyframeProp>();
            ScaleMaxKFP = reader.ReadBlock<ParticleKeyframeProp>();
            RotationMinKFP = reader.ReadBlock<ParticleKeyframeProp>();
            RotationMaxKFP = reader.ReadBlock<ParticleKeyframeProp>();
            Falloff = reader.ReadSingle();
            HDRMult = reader.ReadSingle();
            LightingType = reader.ReadInt32();
            ColourTintFromParticle = reader.ReadByte();
            SortWithParticles = reader.ReadByte();
            UseGroundFogColour = reader.ReadByte();
            UseEffectEvoValues = reader.ReadByte();
        }
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            base.Write(writer, parameters);

            // write structure data
            writer.WriteBlock(RGBTintMinKFP);
            writer.WriteBlock(RGBTintMaxKFP);
            writer.WriteBlock(DensityRangeKFP);
            writer.WriteBlock(ScaleMinKFP);
            writer.WriteBlock(ScaleMaxKFP);
            writer.WriteBlock(RotationMinKFP);
            writer.WriteBlock(RotationMaxKFP);
            writer.Write(Falloff);
            writer.Write(HDRMult);
            writer.Write(LightingType);
            writer.Write(ColourTintFromParticle);
            writer.Write(SortWithParticles);
            writer.Write(UseGroundFogColour);
            writer.Write(UseEffectEvoValues);
        }
        public override void WriteXml(StringBuilder sb, int indent)
        {
            base.WriteXml(sb, indent);
            YptXml.ValueTag(sb, indent, "Falloff", FloatUtil.ToString(Falloff));
            YptXml.ValueTag(sb, indent, "HDRMult", FloatUtil.ToString(HDRMult));
            YptXml.ValueTag(sb, indent, "LightingType", LightingType.ToString());
            YptXml.ValueTag(sb, indent, "ColourTintFromParticle", ColourTintFromParticle.ToString());
            YptXml.ValueTag(sb, indent, "SortWithParticles", SortWithParticles.ToString());
            YptXml.ValueTag(sb, indent, "UseGroundFogColour", UseGroundFogColour.ToString());
            YptXml.ValueTag(sb, indent, "UseEffectEvoValues", UseEffectEvoValues.ToString());
            if (RGBTintMinKFP != null)
            {
                YptXml.OpenTag(sb, indent, "RGBTintMinKFP");
                RGBTintMinKFP.WriteXml(sb, indent + 1);
                YptXml.CloseTag(sb, indent, "RGBTintMinKFP");
            }
            if (RGBTintMaxKFP != null)
            {
                YptXml.OpenTag(sb, indent, "RGBTintMaxKFP");
                RGBTintMaxKFP.WriteXml(sb, indent + 1);
                YptXml.CloseTag(sb, indent, "RGBTintMaxKFP");
            }
            if (DensityRangeKFP != null)
            {
                YptXml.OpenTag(sb, indent, "DensityRangeKFP");
                DensityRangeKFP.WriteXml(sb, indent + 1);
                YptXml.CloseTag(sb, indent, "DensityRangeKFP");
            }
            if (ScaleMinKFP != null)
            {
                YptXml.OpenTag(sb, indent, "ScaleMinKFP");
                ScaleMinKFP.WriteXml(sb, indent + 1);
                YptXml.CloseTag(sb, indent, "ScaleMinKFP");
            }
            if (ScaleMaxKFP != null)
            {
                YptXml.OpenTag(sb, indent, "ScaleMaxKFP");
                ScaleMaxKFP.WriteXml(sb, indent + 1);
                YptXml.CloseTag(sb, indent, "ScaleMaxKFP");
            }
            if (RotationMinKFP != null)
            {
                YptXml.OpenTag(sb, indent, "RotationMinKFP");
                RotationMinKFP.WriteXml(sb, indent + 1);
                YptXml.CloseTag(sb, indent, "RotationMinKFP");
            }
            if (RotationMaxKFP != null)
            {
                YptXml.OpenTag(sb, indent, "RotationMaxKFP");
                RotationMaxKFP.WriteXml(sb, indent + 1);
                YptXml.CloseTag(sb, indent, "RotationMaxKFP");
            }
        }
        public override void ReadXml(XmlNode node)
        {
            base.ReadXml(node);
            Falloff = Xml.GetChildFloatAttribute(node, "Falloff");
            HDRMult = Xml.GetChildFloatAttribute(node, "HDRMult");
            LightingType = Xml.GetChildIntAttribute(node, "LightingType");
            ColourTintFromParticle = (byte)Xml.GetChildUIntAttribute(node, "ColourTintFromParticle");
            SortWithParticles = (byte)Xml.GetChildUIntAttribute(node, "SortWithParticles");
            UseGroundFogColour = (byte)Xml.GetChildUIntAttribute(node, "UseGroundFogColour");
            UseEffectEvoValues = (byte)Xml.GetChildUIntAttribute(node, "UseEffectEvoValues");

            RGBTintMinKFP = new ParticleKeyframeProp();
            var pnode0 = node.SelectSingleNode("RGBTintMinKFP");
            if (pnode0 != null)
            {
                RGBTintMinKFP.ReadXml(pnode0);
            }

            RGBTintMaxKFP = new ParticleKeyframeProp();
            var pnode1 = node.SelectSingleNode("RGBTintMaxKFP");
            if (pnode1 != null)
            {
                RGBTintMaxKFP.ReadXml(pnode1);
            }

            DensityRangeKFP = new ParticleKeyframeProp();
            var pnode2 = node.SelectSingleNode("DensityRangeKFP");
            if (pnode2 != null)
            {
                DensityRangeKFP.ReadXml(pnode2);
            }

            ScaleMinKFP = new ParticleKeyframeProp();
            var pnode3 = node.SelectSingleNode("ScaleMinKFP");
            if (pnode3 != null)
            {
                ScaleMinKFP.ReadXml(pnode3);
            }

            ScaleMaxKFP = new ParticleKeyframeProp();
            var pnode4 = node.SelectSingleNode("ScaleMaxKFP");
            if (pnode4 != null)
            {
                ScaleMaxKFP.ReadXml(pnode4);
            }

            RotationMinKFP = new ParticleKeyframeProp();
            var pnode5 = node.SelectSingleNode("RotationMinKFP");
            if (pnode5 != null)
            {
                RotationMinKFP.ReadXml(pnode5);
            }

            RotationMaxKFP = new ParticleKeyframeProp();
            var pnode6 = node.SelectSingleNode("RotationMaxKFP");
            if (pnode6 != null)
            {
                RotationMaxKFP.ReadXml(pnode6);
            }

            CreateKeyframeProps(RGBTintMinKFP, RGBTintMaxKFP, DensityRangeKFP, ScaleMinKFP, ScaleMaxKFP, RotationMinKFP, RotationMaxKFP);
        }

        public override Tuple<long, IResourceBlock>[] GetParts()
        {
            return new Tuple<long, IResourceBlock>[] {
                new Tuple<long, IResourceBlock>(16, KeyframeProps),
                new Tuple<long, IResourceBlock>(48, RGBTintMinKFP),
                new Tuple<long, IResourceBlock>(192, RGBTintMaxKFP),
                new Tuple<long, IResourceBlock>(336, DensityRangeKFP),
                new Tuple<long, IResourceBlock>(480, ScaleMinKFP),
                new Tuple<long, IResourceBlock>(624, ScaleMaxKFP),
                new Tuple<long, IResourceBlock>(768, RotationMinKFP),
                new Tuple<long, IResourceBlock>(912, RotationMaxKFP)
            };
        }
    }

    [TC(typeof(EXP))]
    public class ParticleBehaviourRiver : ParticleBehaviour
    {
        // ptxu_River
        public override long BlockLength => 0x40;

        // structure data
        public float VelocityMult { get; set; }
        public float Influence { get; set; }
        public ulong padding00 { get; set; }

        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            base.Read(reader, parameters);

            // read structure data
            VelocityMult = reader.ReadSingle();
            Influence = reader.ReadSingle();
            padding00 = reader.ReadUInt64();
        }
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            base.Write(writer, parameters);

            // write structure data
            writer.Write(VelocityMult);
            writer.Write(Influence);
            writer.Write(padding00);
        }
        public override void WriteXml(StringBuilder sb, int indent)
        {
            base.WriteXml(sb, indent);
            YptXml.ValueTag(sb, indent, "VelocityMult", FloatUtil.ToString(VelocityMult));
            YptXml.ValueTag(sb, indent, "Influence", FloatUtil.ToString(Influence));
        }
        public override void ReadXml(XmlNode node)
        {
            base.ReadXml(node);
            VelocityMult = Xml.GetChildFloatAttribute(node, "VelocityMult");
            Influence = Xml.GetChildFloatAttribute(node, "Influence");
        }
    }

    [TC(typeof(EXP))]
    public class ParticleBehaviourDecalPool : ParticleBehaviour
    {
        // ptxu_DecalPool
        public override long BlockLength => 0x50;

        // structure data
        public float VelocityThreshold { get; set; }
        public int LiquidType { get; set; }
        public int DecalID { get; set; }
        public float StartSize { get; set; }
        public float EndSize { get; set; }
        public float GrowthRate { get; set; }
        public ulong padding00 { get; set; }

        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            base.Read(reader, parameters);

            // read structure data
            VelocityThreshold = reader.ReadSingle();
            LiquidType = reader.ReadInt32();
            DecalID = reader.ReadInt32();
            StartSize = reader.ReadSingle();
            EndSize = reader.ReadSingle();
            GrowthRate = reader.ReadSingle();
            padding00 = reader.ReadUInt64();
        }
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            base.Write(writer, parameters);

            // write structure data
            writer.Write(VelocityThreshold);
            writer.Write(LiquidType);
            writer.Write(DecalID);
            writer.Write(StartSize);
            writer.Write(EndSize);
            writer.Write(GrowthRate);
            writer.Write(padding00);
        }
        public override void WriteXml(StringBuilder sb, int indent)
        {
            base.WriteXml(sb, indent);
            YptXml.ValueTag(sb, indent, "VelocityThreshold", FloatUtil.ToString(VelocityThreshold));
            YptXml.ValueTag(sb, indent, "LiquidType", LiquidType.ToString());
            YptXml.ValueTag(sb, indent, "DecalID", DecalID.ToString());
            YptXml.ValueTag(sb, indent, "StartSize", FloatUtil.ToString(StartSize));
            YptXml.ValueTag(sb, indent, "EndSize", FloatUtil.ToString(EndSize));
            YptXml.ValueTag(sb, indent, "GrowthRate", FloatUtil.ToString(GrowthRate));
        }
        public override void ReadXml(XmlNode node)
        {
            base.ReadXml(node);
            VelocityThreshold = Xml.GetChildFloatAttribute(node, "VelocityThreshold");
            LiquidType = Xml.GetChildIntAttribute(node, "LiquidType");
            DecalID = Xml.GetChildIntAttribute(node, "DecalID");
            StartSize = Xml.GetChildFloatAttribute(node, "StartSize");
            EndSize = Xml.GetChildFloatAttribute(node, "EndSize");
            GrowthRate = Xml.GetChildFloatAttribute(node, "GrowthRate");
        }
    }

    [TC(typeof(EXP))]
    public class ParticleBehaviourLiquid : ParticleBehaviour
    {
        // ptxu_Liquid
        public override long BlockLength => 0x50;

        // structure data
        public float VelocityThreshold { get; set; }
        public int LiquidType { get; set; }
        public float PoolStartSize { get; set; }
        public float PoolEndSize { get; set; }
        public float PoolGrowthRate { get; set; }
        public float TrailWidthMin { get; set; }
        public float TrailWidthMax { get; set; }
        public uint padding00 { get; set; }

        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            base.Read(reader, parameters);

            // read structure data
            VelocityThreshold = reader.ReadSingle();
            LiquidType = reader.ReadInt32();
            PoolStartSize = reader.ReadSingle();
            PoolEndSize = reader.ReadSingle();
            PoolGrowthRate = reader.ReadSingle();
            TrailWidthMin = reader.ReadSingle();
            TrailWidthMax = reader.ReadSingle();
            padding00 = reader.ReadUInt32();
        }
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            base.Write(writer, parameters);

            // write structure data
            writer.Write(VelocityThreshold);
            writer.Write(LiquidType);
            writer.Write(PoolStartSize);
            writer.Write(PoolEndSize);
            writer.Write(PoolGrowthRate);
            writer.Write(TrailWidthMin);
            writer.Write(TrailWidthMax);
            writer.Write(padding00);
        }
        public override void WriteXml(StringBuilder sb, int indent)
        {
            base.WriteXml(sb, indent);
            YptXml.ValueTag(sb, indent, "VelocityThreshold", FloatUtil.ToString(VelocityThreshold));
            YptXml.ValueTag(sb, indent, "LiquidType", LiquidType.ToString());
            YptXml.ValueTag(sb, indent, "PoolStartSize", FloatUtil.ToString(PoolStartSize));
            YptXml.ValueTag(sb, indent, "PoolEndSize", FloatUtil.ToString(PoolEndSize));
            YptXml.ValueTag(sb, indent, "PoolGrowthRate", FloatUtil.ToString(PoolGrowthRate));
            YptXml.ValueTag(sb, indent, "TrailWidthMin", FloatUtil.ToString(TrailWidthMin));
            YptXml.ValueTag(sb, indent, "TrailWidthMax", FloatUtil.ToString(TrailWidthMax));
        }
        public override void ReadXml(XmlNode node)
        {
            base.ReadXml(node);
            VelocityThreshold = Xml.GetChildFloatAttribute(node, "VelocityThreshold");
            LiquidType = Xml.GetChildIntAttribute(node, "LiquidType");
            PoolStartSize = Xml.GetChildFloatAttribute(node, "PoolStartSize");
            PoolEndSize = Xml.GetChildFloatAttribute(node, "PoolEndSize");
            PoolGrowthRate = Xml.GetChildFloatAttribute(node, "PoolGrowthRate");
            TrailWidthMin = Xml.GetChildFloatAttribute(node, "TrailWidthMin");
            TrailWidthMax = Xml.GetChildFloatAttribute(node, "TrailWidthMax");
        }
    }












    public enum ParticleShaderVarType : byte
    {
        Vector2 = 2,
        Vector4 = 4,
        Texture = 6,
        Keyframe = 7,
    }

    [TC(typeof(EXP))] public class ParticleShaderVar : ResourceSystemBlock, IResourceXXSystemBlock, IMetaXmlItem
    {
        // datBase
        // ptxShaderVar
        public override long BlockLength => 24;

        // structure data
        public uint VFT { get; set; }
        public uint Unknown_4h = 1; // 0x00000001
        public ulong Unknown_8h; // 0x0000000000000000
        public MetaHash Name { get; set; }
        public ParticleShaderVarType Type { get; set; }
        public byte Unknown_15h; // 0x00
        public ushort Unknown_16h; // 0x0000

        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            // read structure data
            VFT = reader.ReadUInt32();
            Unknown_4h = reader.ReadUInt32();
            Unknown_8h = reader.ReadUInt64();
            Name = reader.ReadUInt32();
            Type = (ParticleShaderVarType)reader.ReadByte();
            Unknown_15h = reader.ReadByte();
            Unknown_16h = reader.ReadUInt16();

            //if (Unknown_4h != 1)
            //{ }//no hit
            //if (Unknown_8h != 0)
            //{ }//no hit
            //switch (Name) //parameter name
            //{
            //    case 0xea057402: // 
            //    case 0x0b3045be: // softness
            //    case 0x91bf3028: // superalpha
            //    case 0x4a8a0a28: // directionalmult
            //    case 0xf8338e85: // ambientmult
            //    case 0xbfd98c1d: // shadowamount
            //    case 0xc6fe034a: // extralightmult
            //    case 0xf03acb8c: // camerabias
            //    case 0x81634888: // camerashrink
            //    case 0xb695f45c: // normalarc
            //    case 0x403390ea: // 
            //    case 0x18ca6c12: // softnesscurve
            //    case 0x1458f27b: // softnessshadowmult
            //    case 0xa781a38b: // softnessshadowoffset
            //    case 0x77b842ed: // normalmapmult
            //    case 0x7b483bc5: // 
            //    case 0x6a1dbec3: // 
            //    case 0xba5af058: // 
            //    case 0xdf7cc018: // refractionmap
            //    case 0xb36327d1: // normalspecmap
            //    case 0x0df47048: // diffusetex2
            //        break;
            //    default:
            //        break;//no hit
            //}
            //if (Unknown_15h != 0)
            //{ }//no hit
            //if (Unknown_16h != 0)
            //{ }//no hit
        }
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            // write structure data
            writer.Write(VFT);
            writer.Write(Unknown_4h);
            writer.Write(Unknown_8h);
            writer.Write(Name);
            writer.Write((byte)Type);
            writer.Write(Unknown_15h);
            writer.Write(Unknown_16h);
        }
        public virtual void WriteXml(StringBuilder sb, int indent)
        {
            YptXml.ValueTag(sb, indent, "Type", Type.ToString());
            YptXml.StringTag(sb, indent, "Name", YptXml.HashString(Name));
        }
        public virtual void ReadXml(XmlNode node)
        {
            Type = Xml.GetEnumValue<ParticleShaderVarType>(Xml.GetChildStringAttribute(node, "Type"));
            Name = XmlMeta.GetHash(Xml.GetChildInnerText(node, "Name"));
        }
        public static void WriteXmlNode(ParticleShaderVar v, StringBuilder sb, int indent, string name)
        {
            if (v != null)
            {
                YptXml.OpenTag(sb, indent, name);
                v.WriteXml(sb, indent + 1);
                YptXml.CloseTag(sb, indent, name);
            }
        }
        public static ParticleShaderVar ReadXmlNode(XmlNode node)
        {
            if (node != null)
            {
                var typestr = Xml.GetChildStringAttribute(node, "Type");
                var type = Xml.GetEnumValue<ParticleShaderVarType>(typestr);
                var s = Create(type);
                if (s != null)
                {
                    s.ReadXml(node);
                }
                return s;
            }
            return null;
        }

        public IResourceSystemBlock GetType(ResourceDataReader reader, params object[] parameters)
        {
            reader.Position += 20;
            var type = (ParticleShaderVarType)reader.ReadByte();
            reader.Position -= 21;

            return Create(type);
        }
        public static ParticleShaderVar Create(ParticleShaderVarType type)
        {
            switch (type)
            {
                case ParticleShaderVarType.Vector2:
                case ParticleShaderVarType.Vector4: return new ParticleShaderVarVector();
                case ParticleShaderVarType.Texture: return new ParticleShaderVarTexture();
                case ParticleShaderVarType.Keyframe: return new ParticleShaderVarKeyframe();
                default: return null;// throw new Exception("Unknown shader var type");
            }
        }

        public override string ToString()
        {
            return Name.ToString() + ": " + Type.ToString();
        }

    }

    [TC(typeof(EXP))] public class ParticleShaderVarVector : ParticleShaderVar
    {
        // ptxShaderVarVector
        public override long BlockLength => 0x40;

        // structure data
        public uint Unknown_18h { get; set; } // 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 18, 19, 21, 22, 32       //shader var index..?
        public uint Unknown_1Ch; // 0x00000000
        public uint Unknown_20h; // 0x00000000
        public uint Unknown_24h; // 0x00000000
        public uint Unknown_28h; // 0x00000000
        public uint Unknown_2Ch; // 0x00000000
        public float Unknown_30h { get; set; } // 0, 0.1f, 0.2f, ..., 1.0f, 2.0f, ...
        public float Unknown_34h { get; set; } // 0, 0.5f, 0.996f, 1.0f
        public float Unknown_38h { get; set; } // 0, 0.1f, 0.2f, ..., 1.0f, ...
        public uint Unknown_3Ch; // 0x00000000

        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            base.Read(reader, parameters);

            // read structure data
            Unknown_18h = reader.ReadUInt32();
            Unknown_1Ch = reader.ReadUInt32();
            Unknown_20h = reader.ReadUInt32();
            Unknown_24h = reader.ReadUInt32();
            Unknown_28h = reader.ReadUInt32();
            Unknown_2Ch = reader.ReadUInt32();
            Unknown_30h = reader.ReadSingle();
            Unknown_34h = reader.ReadSingle();
            Unknown_38h = reader.ReadSingle();
            Unknown_3Ch = reader.ReadUInt32();

            //switch (Unknown_18h) //shader var index..?
            //{
            //    case 32:
            //    case 22:
            //    case 21:
            //    case 19:
            //    case 18:
            //    case 14:
            //    case 13:
            //    case 12:
            //    case 11:
            //    case 10:
            //    case 9:
            //    case 8:
            //    case 7:
            //    case 6:
            //    case 5:
            //    case 4:
            //        break;
            //    default:
            //        break;//no hit
            //}
            //if (Unknown_1Ch != 0)
            //{ }//no hit
            //if (Unknown_20h != 0)
            //{ }//no hit
            //if (Unknown_24h != 0)
            //{ }//no hit
            //if (Unknown_28h != 0)
            //{ }//no hit
            //if (Unknown_2Ch != 0)
            //{ }//no hit
            switch (Unknown_30h)
            {
                case 1.0f:
                case 0.1f:
                case 0.2f:
                case 0.02f:
                case 0.01f:
                case 2.0f:
                case 0.4f:
                case 0:
                    break;
                default:
                    break;//and more..
            }
            //switch (Unknown_34h)
            //{
            //    case 0:
            //    case 1.0f:
            //    case 0.5f:
            //    case 0.996f:
            //        break;
            //    default:
            //        break;//no hit
            //}
            switch (Unknown_38h)
            {
                case 0:
                case 1.0f:
                case 0.5f:
                case 0.1f:
                case 0.2f:
                case 0.7f:
                    break;
                default:
                    break;//more
            }
            //if (Unknown_3Ch != 0)
            //{ }//no hit
        }
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            base.Write(writer, parameters);

            // write structure data
            writer.Write(Unknown_18h);
            writer.Write(Unknown_1Ch);
            writer.Write(Unknown_20h);
            writer.Write(Unknown_24h);
            writer.Write(Unknown_28h);
            writer.Write(Unknown_2Ch);
            writer.Write(Unknown_30h);
            writer.Write(Unknown_34h);
            writer.Write(Unknown_38h);
            writer.Write(Unknown_3Ch);
        }
        public override void WriteXml(StringBuilder sb, int indent)
        {
            base.WriteXml(sb, indent);
            YptXml.ValueTag(sb, indent, "Unknown18", Unknown_18h.ToString());
            YptXml.ValueTag(sb, indent, "Unknown30", FloatUtil.ToString(Unknown_30h));
            YptXml.ValueTag(sb, indent, "Unknown34", FloatUtil.ToString(Unknown_34h));
            YptXml.ValueTag(sb, indent, "Unknown38", FloatUtil.ToString(Unknown_38h));
        }
        public override void ReadXml(XmlNode node)
        {
            base.ReadXml(node);
            Unknown_18h = Xml.GetChildUIntAttribute(node, "Unknown18");
            Unknown_30h = Xml.GetChildFloatAttribute(node, "Unknown30");
            Unknown_34h = Xml.GetChildFloatAttribute(node, "Unknown34");
            Unknown_38h = Xml.GetChildFloatAttribute(node, "Unknown38");
        }
    }

    [TC(typeof(EXP))] public class ParticleShaderVarTexture : ParticleShaderVar
    {
        // ptxShaderVarTexture
        public override long BlockLength => 0x40;

        // structure data
        public uint Unknown_18h { get; set; } // 3, 4, 6, 7       //shader var index..?
        public uint Unknown_1Ch; // 0x00000000
        public uint Unknown_20h; // 0x00000000
        public uint Unknown_24h; // 0x00000000
        public ulong TexturePointer { get; set; }
        public ulong TextureNamePointer { get; set; }
        public MetaHash TextureNameHash { get; set; }
        public uint Unknown_3Ch { get; set; } // 0, 1

        // reference data
        public Texture Texture { get; set; }
        public string_r TextureName { get; set; }

        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            base.Read(reader, parameters);

            // read structure data
            Unknown_18h = reader.ReadUInt32();
            Unknown_1Ch = reader.ReadUInt32();
            Unknown_20h = reader.ReadUInt32();
            Unknown_24h = reader.ReadUInt32();
            TexturePointer = reader.ReadUInt64();
            TextureNamePointer = reader.ReadUInt64();
            TextureNameHash = reader.ReadUInt32();
            Unknown_3Ch = reader.ReadUInt32();

            // read reference data
            Texture = reader.ReadBlockAt<Texture>(TexturePointer);
            TextureName = reader.ReadBlockAt<string_r>(TextureNamePointer);


            //switch (Unknown_18h) //shader var index..?
            //{
            //    case 7:
            //    case 6:
            //    case 4:
            //    case 3:
            //        break;
            //    default:
            //        break;//no hit
            //}
            //if (Unknown_1Ch != 0)
            //{ }//no hit
            //if (Unknown_20h != 0)
            //{ }//no hit
            //if (Unknown_24h != 0)
            //{ }//no hit
            //switch (TextureNameHash)
            //{
            //    case 0:
            //    case 0xda1c24ad: // ptfx_gloop
            //    case 0xc4e50054: // ptfx_water_splashes_sheet
            //        break;
            //    default:
            //        break;//and more...
            //}
            //if (TextureNameHash != JenkHash.GenHash(TextureName?.ToString() ?? ""))
            //{ }//no hit
            //switch (Unknown_3Ch)
            //{
            //    case 0:
            //    case 1:
            //        break;
            //    default:
            //        break;//no hit
            //}
        }
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            base.Write(writer, parameters);

            // update structure data
            TexturePointer = (ulong)(Texture != null ? Texture.FilePosition : 0);
            TextureNamePointer = (ulong)(TextureName != null ? TextureName.FilePosition : 0);

            // write structure data
            writer.Write(Unknown_18h);
            writer.Write(Unknown_1Ch);
            writer.Write(Unknown_20h);
            writer.Write(Unknown_24h);
            writer.Write(TexturePointer);
            writer.Write(TextureNamePointer);
            writer.Write(TextureNameHash);
            writer.Write(Unknown_3Ch);
        }
        public override void WriteXml(StringBuilder sb, int indent)
        {
            base.WriteXml(sb, indent);
            YptXml.ValueTag(sb, indent, "Unknown18", Unknown_18h.ToString());
            YptXml.ValueTag(sb, indent, "Unknown3C", Unknown_3Ch.ToString());
            YptXml.StringTag(sb, indent, "TextureName", YptXml.XmlEscape(TextureName?.Value ?? ""));
        }
        public override void ReadXml(XmlNode node)
        {
            base.ReadXml(node);
            Unknown_18h = Xml.GetChildUIntAttribute(node, "Unknown18");
            Unknown_3Ch = Xml.GetChildUIntAttribute(node, "Unknown3C");
            TextureName = (string_r)Xml.GetChildInnerText(node, "TextureName"); if (TextureName.Value == null) TextureName = null;
            TextureNameHash = JenkHash.GenHash(TextureName?.Value ?? "");
        }

        public override IResourceBlock[] GetReferences()
        {
            var list = new List<IResourceBlock>(base.GetReferences());
            if (Texture != null) list.Add(Texture);
            if (TextureName != null) list.Add(TextureName);
            return list.ToArray();
        }
    }

    [TC(typeof(EXP))] public class ParticleShaderVarKeyframe : ParticleShaderVar
    {
        // ptxShaderVarKeyframe
        public override long BlockLength => 0x50;

        // structure data
        public uint Unknown_18h { get; set; } // 9, 14, 15, 16, 17, 20, 23, 31       //shader var index..?
        public uint Unknown_1Ch = 1; // 0x00000001
        public ulong Unknown_20h; // 0x0000000000000000
        public ResourceSimpleList64<ParticleShaderVarKeyframeItem> Items { get; set; }
        public ulong Unknown_38h; // 0x0000000000000000
        public ulong Unknown_40h; // 0x0000000000000000
        public ulong Unknown_48h; // 0x0000000000000000

        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            base.Read(reader, parameters);

            // read structure data
            Unknown_18h = reader.ReadUInt32();
            Unknown_1Ch = reader.ReadUInt32();
            Unknown_20h = reader.ReadUInt64();
            Items = reader.ReadBlock<ResourceSimpleList64<ParticleShaderVarKeyframeItem>>();
            Unknown_38h = reader.ReadUInt64();
            Unknown_40h = reader.ReadUInt64();
            Unknown_48h = reader.ReadUInt64();

            //switch (Unknown_18h) //shader var index..?
            //{
            //    case 31:
            //    case 23:
            //    case 20:
            //    case 17:
            //    case 16:
            //    case 15:
            //    case 14:
            //    case 9:
            //        break;
            //    default:
            //        break;//no hit
            //}
            //if (Unknown_1Ch != 1)
            //{ }//no hit
            //if (Unknown_20h != 0)
            //{ }//no hit
            //if (Unknown_38h != 0)
            //{ }//no hit
            //if (Unknown_40h != 0)
            //{ }//no hit
            //if (Unknown_48h != 0)
            //{ }//no hit

        }
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            base.Write(writer, parameters);

            // write structure data
            writer.Write(Unknown_18h);
            writer.Write(Unknown_1Ch);
            writer.Write(Unknown_20h);
            writer.WriteBlock(Items);
            writer.Write(Unknown_38h);
            writer.Write(Unknown_40h);
            writer.Write(Unknown_48h);
        }
        public override void WriteXml(StringBuilder sb, int indent)
        {
            base.WriteXml(sb, indent);
            YptXml.ValueTag(sb, indent, "Unknown18", Unknown_18h.ToString());
            YptXml.WriteItemArray(sb, Items?.data_items, indent, "Items");
        }
        public override void ReadXml(XmlNode node)
        {
            base.ReadXml(node);
            Unknown_18h = Xml.GetChildUIntAttribute(node, "Unknown18");
            Items = new ResourceSimpleList64<ParticleShaderVarKeyframeItem>();
            Items.data_items = XmlMeta.ReadItemArray<ParticleShaderVarKeyframeItem>(node, "Items");
        }

        public override Tuple<long, IResourceBlock>[] GetParts()
        {
            return new Tuple<long, IResourceBlock>[] {
                new Tuple<long, IResourceBlock>(0x28, Items)
            };
        }
    }

    [TC(typeof(EXP))] public class ParticleShaderVarKeyframeItem : ResourceSystemBlock, IMetaXmlItem
    {
        public override long BlockLength => 0x20;

        // structure data
        public float Unknown_0h { get; set; }
        public float Unknown_4h { get; set; }
        public ulong Unknown_8h; // 0x0000000000000000
        public float Unknown_10h { get; set; }
        public uint Unknown_14h; // 0x00000000
        public ulong Unknown_18h; // 0x0000000000000000

        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            // read structure data
            Unknown_0h = reader.ReadSingle();
            Unknown_4h = reader.ReadSingle();
            Unknown_8h = reader.ReadUInt64();
            Unknown_10h = reader.ReadSingle();
            Unknown_14h = reader.ReadUInt32();
            Unknown_18h = reader.ReadUInt64();

            switch (Unknown_0h)
            {
                case 0:
                case 0.2f:
                case 1.0f:
                case 0.149759f:
                case 0.63285f:
                    break;
                default:
                    break;//and more..
            }
            switch (Unknown_4h)
            {
                case 0:
                case 5.0f:
                case 1.25f:
                case 6.67739534f:
                case 2.07000327f:
                    break;
                default:
                    break;//and more..
            }
            //if (Unknown_8h != 0)
            //{ }//no hit
            switch (Unknown_10h)
            {
                case 20.0f:
                case 1.0f:
                case 0.2f:
                case 0.8f:
                case 1.080267f:
                case 0:
                    break;
                default:
                    break;//and more..
            }
            //if (Unknown_14h != 0)
            //{ }//no hit
            //if (Unknown_18h != 0)
            //{ }//no hit
        }
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            // write structure data
            writer.Write(Unknown_0h);
            writer.Write(Unknown_4h);
            writer.Write(Unknown_8h);
            writer.Write(Unknown_10h);
            writer.Write(Unknown_14h);
            writer.Write(Unknown_18h);
        }
        public void WriteXml(StringBuilder sb, int indent)
        {
            YptXml.ValueTag(sb, indent, "Unknown0", FloatUtil.ToString(Unknown_0h));
            YptXml.ValueTag(sb, indent, "Unknown4", FloatUtil.ToString(Unknown_4h));
            YptXml.ValueTag(sb, indent, "Unknown10", FloatUtil.ToString(Unknown_10h));
        }
        public void ReadXml(XmlNode node)
        {
            Unknown_0h = Xml.GetChildFloatAttribute(node, "Unknown0");
            Unknown_4h = Xml.GetChildFloatAttribute(node, "Unknown4");
            Unknown_10h = Xml.GetChildFloatAttribute(node, "Unknown10");
        }
    }









}
