﻿#if DEBUG
namespace FreelancerModStudio
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    using FreelancerModStudio.Data;
    using FreelancerModStudio.Data.INI;
    using FreelancerModStudio.Data.IO;
    using FreelancerModStudio.SystemDesigner;

    // this class is for developers testing purposes only 
    internal class DevTest
    {
        // create template for freelancer\data path (inis)
        public class IniDataTemplate
        {
            public string Path;
            public List<IniBlock> Blocks = new List<IniBlock>();
        }

        private static readonly List<IniDataTemplate> DataList = new List<IniDataTemplate>();

        public static void CreateTemplate(string path)
        {
            CreateTemplate(path, path.Length + 1);

            Template template = new Template();
            foreach (IniDataTemplate iniDataTemplate in DataList)
            {
                // each file
                Table<string, Template.Block> templateBlocks = new Table<string, Template.Block>(StringComparer.OrdinalIgnoreCase);
                foreach (IniBlock block in iniDataTemplate.Blocks)
                {
                    // each block
                    Template.Options templateOptions = new Template.Options();
                    foreach (KeyValuePair<string, List<IniOption>> option in block.Options)
                    {
                        // each option
                        templateOptions.Add(new Template.Option
                            {
                                Multiple = option.Value.Count > 1,
                                Name = option.Key
                            });
                    }

                    int blockIndex = templateBlocks.IndexOf(block.Name);
                    if (blockIndex != -1)
                    {
                        // integration options
                        foreach (Template.Option option in templateOptions)
                        {
                            int optionIndex = templateBlocks.Values[blockIndex].Options.IndexOf(option.Name);
                            if (optionIndex != -1)
                            {
                                // change to multiple options
                                if (option.Multiple)
                                {
                                    templateBlocks.Values[blockIndex].Options[optionIndex].Multiple = true;
                                }
                            }
                            else
                            {
                                // add missing option
                                templateBlocks.Values[blockIndex].Options.Add(option);
                            }
                        }

                        // change to multiple blocks
                        templateBlocks.Values[blockIndex].Multiple = true;

                        if (templateBlocks.Values[blockIndex].Options.IndexOf("nickname") != -1)
                        {
                            templateBlocks.Values[blockIndex].Identifier = "nickname";
                        }

                        // sort options after integration
                        // templateBlocks.Values[blockIndex].Options.Sort();
                    }
                    else
                    {
                        // add new block
                        templateBlocks.Add(new Template.Block
                            {
                                Name = block.Name,
                                Options = templateOptions
                            });
                    }
                }

                Template.File file = new Template.File
                    {
                        Name = Path.GetFileName(iniDataTemplate.Path.ToLowerInvariant()),
                        Paths = new List<string>
                            {
                                iniDataTemplate.Path.ToLowerInvariant()
                            },
                        Blocks = templateBlocks
                    };
                template.Data.Files.Add(file);
            }

            template.Save("newTemplate.xml");
        }

        private static void CreateTemplate(string path, int dataPathIndex)
        {
            foreach (string file in Directory.GetFiles(path, "*.ini"))
            {
                CreateTemplateFromFile(file, dataPathIndex);
            }

            foreach (string directory in Directory.GetDirectories(path))
            {
                CreateTemplate(directory, dataPathIndex);
            }
        }

        #region "File Groups"

        private static readonly string[][] FileGroups =
            {
                // fuses
                new[]
                    {
                        "fx\\fuse.ini", "fx\\fuse_br_battleship.ini", "fx\\fuse_br_destroyer.ini",
                        "fx\\fuse_br_gunship.ini", "fx\\fuse_ku_battleship.ini", "fx\\fuse_ku_destroyer.ini",
                        "fx\\fuse_ku_gunship.ini", "fx\\fuse_li_battleship.ini", "fx\\fuse_li_cruiser.ini",
                        "fx\\fuse_li_dreadnought.ini", "fx\\fuse_rh_battleship.ini", "fx\\fuse_rh_cruiser.ini",
                        "fx\\fuse_rh_gunship.ini", "fx\\fuse_or_osiris.ini", "fx\\fuse_transport.ini",
                        "fx\\fuse_suprise_solar.ini", "fx\\fuse_freeport7.ini"
                    },

                // systems
                new[]
                    {
                        "universe\\systems\\st03b\\st03b.ini", "universe\\systems\\st03\\st03.ini",
                        "universe\\systems\\st02c\\st02c.ini", "universe\\systems\\st02\\st02.ini",
                        "universe\\systems\\st01\\st01.ini", "universe\\systems\\rh05\\rh05.ini",
                        "universe\\systems\\rh04\\rh04.ini", "universe\\systems\\rh03\\rh03.ini",
                        "universe\\systems\\rh02\\rh02.ini", "universe\\systems\\rh01\\rh01.ini",
                        "universe\\systems\\li05\\li05.ini", "universe\\systems\\li04\\li04.ini",
                        "universe\\systems\\li03\\li03.ini", "universe\\systems\\li02\\li02.ini",
                        "universe\\systems\\li01\\li01.ini", "universe\\systems\\ku07\\ku07.ini",
                        "universe\\systems\\ku06\\ku06.ini", "universe\\systems\\ku05\\ku05.ini",
                        "universe\\systems\\ku04\\ku04.ini", "universe\\systems\\ku03\\ku03.ini",
                        "universe\\systems\\ku02\\ku02.ini", "universe\\systems\\ku01\\ku01.ini",
                        "universe\\systems\\iw06\\iw06.ini", "universe\\systems\\iw05\\iw05.ini",
                        "universe\\systems\\iw04\\iw04.ini", "universe\\systems\\iw03\\iw03.ini",
                        "universe\\systems\\iw02\\iw02.ini", "universe\\systems\\iw01\\iw01.ini",
                        "universe\\systems\\intro\\intro.ini", "universe\\systems\\hi02\\hi02.ini",
                        "universe\\systems\\hi01\\hi01.ini", "universe\\systems\\fp7\\fp7_system.ini",
                        "universe\\systems\\ew06\\ew06.ini", "universe\\systems\\ew05\\ew05.ini",
                        "universe\\systems\\ew04\\ew04.ini", "universe\\systems\\ew03\\ew03.ini",
                        "universe\\systems\\ew02\\ew02.ini", "universe\\systems\\ew01\\ew01.ini",
                        "universe\\systems\\bw10\\bw10.ini", "universe\\systems\\bw09\\bw09.ini",
                        "universe\\systems\\bw08\\bw08.ini", "universe\\systems\\bw07\\bw07.ini",
                        "universe\\systems\\bw06\\bw06.ini", "universe\\systems\\bw05\\bw05.ini",
                        "universe\\systems\\bw04\\bw04.ini", "universe\\systems\\bw03\\bw03.ini",
                        "universe\\systems\\bw02\\bw02.ini", "universe\\systems\\bw01\\bw01.ini",
                        "universe\\systems\\br06\\br06.ini", "universe\\systems\\br05\\br05.ini",
                        "universe\\systems\\br04\\br04.ini", "universe\\systems\\br03\\br03.ini",
                        "universe\\systems\\br02\\br02.ini", "universe\\systems\\br01\\br01.ini"
                    },

                // system bases
                new[]
                    {
                        "universe\\systems\\br_m_mining_base.ini", "universe\\systems\\co_ti_mining_base.ini",
                        "universe\\systems\\gd_im_mining_base.ini",
                        "universe\\systems\\miners\\br_m_beryllium_miner.ini",
                        "universe\\systems\\miners\\br_m_hydrocarbon_miner.ini",
                        "universe\\systems\\miners\\br_m_niobium_miner.ini",
                        "universe\\systems\\miners\\co_khc_cobalt_miner.ini",
                        "universe\\systems\\miners\\co_khc_copper_miner.ini",
                        "universe\\systems\\miners\\co_kt_hydrocarbon_miner.ini",
                        "universe\\systems\\miners\\co_shi_h-fuel_miner.ini",
                        "universe\\systems\\miners\\co_shi_water_miner.ini",
                        "universe\\systems\\miners\\co_ti_water_miner.ini",
                        "universe\\systems\\miners\\gd_gm_h-fuel_miner.ini",
                        "universe\\systems\\miners\\gd_im_copper_miner.ini",
                        "universe\\systems\\miners\\gd_im_oxygen_miner.ini",
                        "universe\\systems\\miners\\gd_im_silver_miner.ini",
                        "universe\\systems\\miners\\gd_im_water_miner.ini",
                        "universe\\systems\\miners\\rh_m_diamond_miner.ini",
                        "universe\\systems\\st03b\\bases\\st03b_01_base.ini",
                        "universe\\systems\\st02\\bases\\st02_01_base.ini",
                        "universe\\systems\\st01\\bases\\st01_01_base.ini",
                        "universe\\systems\\st01\\bases\\st01_02_base.ini",
                        "universe\\systems\\rh05\\bases\\rh05_01_base.ini",
                        "universe\\systems\\rh05\\bases\\rh05_02_base.ini",
                        "universe\\systems\\rh05\\bases\\rh05_03_base.ini",
                        "universe\\systems\\rh05\\bases\\rh05_04_base.ini",
                        "universe\\systems\\rh04\\bases\\rh04_01_base.ini",
                        "universe\\systems\\rh04\\bases\\rh04_02_base.ini",
                        "universe\\systems\\rh04\\bases\\rh04_03_base.ini",
                        "universe\\systems\\rh04\\bases\\rh04_04_base.ini",
                        "universe\\systems\\rh04\\bases\\rh04_05_base.ini",
                        "universe\\systems\\rh03\\bases\\rh03_01_base.ini",
                        "universe\\systems\\rh03\\bases\\rh03_02_base.ini",
                        "universe\\systems\\rh03\\bases\\rh03_03_base.ini",
                        "universe\\systems\\rh03\\bases\\rh03_04_base.ini",
                        "universe\\systems\\rh03\\bases\\rh03_05_base.ini",
                        "universe\\systems\\rh03\\bases\\rh03_06_base.ini",
                        "universe\\systems\\rh02\\bases\\rh02_01_base.ini",
                        "universe\\systems\\rh02\\bases\\rh02_02_base.ini",
                        "universe\\systems\\rh02\\bases\\rh02_03_base.ini",
                        "universe\\systems\\rh02\\bases\\rh02_04_base.ini",
                        "universe\\systems\\rh02\\bases\\rh02_05_base.ini",
                        "universe\\systems\\rh02\\bases\\rh02_06_base.ini",
                        "universe\\systems\\rh02\\bases\\rh02_07_base.ini",
                        "universe\\systems\\rh01\\bases\\rh01_01_base.ini",
                        "universe\\systems\\rh01\\bases\\rh01_02_base.ini",
                        "universe\\systems\\rh01\\bases\\rh01_03_base.ini",
                        "universe\\systems\\rh01\\bases\\rh01_04_base.ini",
                        "universe\\systems\\rh01\\bases\\rh01_05_base.ini",
                        "universe\\systems\\rh01\\bases\\rh01_06_base.ini",
                        "universe\\systems\\rh01\\bases\\rh01_07_base.ini",
                        "universe\\systems\\rh01\\bases\\rh01_08_base.ini",
                        "universe\\systems\\li05\\bases\\li05_01_base.ini",
                        "universe\\systems\\li04\\bases\\li04_01_base.ini",
                        "universe\\systems\\li04\\bases\\li04_02_base.ini",
                        "universe\\systems\\li04\\bases\\li04_03_base.ini",
                        "universe\\systems\\li04\\bases\\li04_04_base.ini",
                        "universe\\systems\\li04\\bases\\li04_05_base.ini",
                        "universe\\systems\\li04\\bases\\li04_06_base.ini",
                        "universe\\systems\\li03\\bases\\li03_01_base.ini",
                        "universe\\systems\\li03\\bases\\li03_02_base.ini",
                        "universe\\systems\\li03\\bases\\li03_03_base.ini",
                        "universe\\systems\\li03\\bases\\li03_04_base.ini",
                        "universe\\systems\\li02\\bases\\li02_01_base.ini",
                        "universe\\systems\\li02\\bases\\li02_02_base.ini",
                        "universe\\systems\\li02\\bases\\li02_03_base.ini",
                        "universe\\systems\\li02\\bases\\li02_04_base.ini",
                        "universe\\systems\\li02\\bases\\li02_05_base.ini",
                        "universe\\systems\\li02\\bases\\li02_06_base.ini",
                        "universe\\systems\\li01\\bases\\li01_01_base.ini",
                        "universe\\systems\\li01\\bases\\li01_02_base.ini",
                        "universe\\systems\\li01\\bases\\li01_03_base.ini",
                        "universe\\systems\\li01\\bases\\li01_04_base.ini",
                        "universe\\systems\\li01\\bases\\li01_05_base.ini",
                        "universe\\systems\\li01\\bases\\li01_06_base.ini",
                        "universe\\systems\\li01\\bases\\li01_07_base.ini",
                        "universe\\systems\\li01\\bases\\li01_08_base.ini",
                        "universe\\systems\\li01\\bases\\li01_09_base.ini",
                        "universe\\systems\\li01\\bases\\li01_10_base.ini",
                        "universe\\systems\\li01\\bases\\li01_11_base.ini",
                        "universe\\systems\\li01\\bases\\li01_12_base.ini",
                        "universe\\systems\\li01\\bases\\li01_13_base.ini",
                        "universe\\systems\\li01\\bases\\li01_14_base.ini",
                        "universe\\systems\\li01\\bases\\li01_15_base.ini",
                        "universe\\systems\\ku07\\bases\\ku07_01_base.ini",
                        "universe\\systems\\ku07\\bases\\ku07_02_base.ini",
                        "universe\\systems\\ku06\\bases\\ku06_01_base.ini",
                        "universe\\systems\\ku05\\bases\\ku05_01_base.ini",
                        "universe\\systems\\ku05\\bases\\ku05_02_base.ini",
                        "universe\\systems\\ku05\\bases\\ku05_03_base.ini",
                        "universe\\systems\\ku05\\bases\\ku05_04_base.ini",
                        "universe\\systems\\ku04\\bases\\ku04_01_base.ini",
                        "universe\\systems\\ku04\\bases\\ku04_02_base.ini",
                        "universe\\systems\\ku04\\bases\\ku04_03_base.ini",
                        "universe\\systems\\ku04\\bases\\ku04_04_base.ini",
                        "universe\\systems\\ku04\\bases\\ku04_05_base.ini",
                        "universe\\systems\\ku04\\bases\\ku04_06_base.ini",
                        "universe\\systems\\ku03\\bases\\ku03_01_base.ini",
                        "universe\\systems\\ku03\\bases\\ku03_02_base.ini",
                        "universe\\systems\\ku03\\bases\\ku03_03_base.ini",
                        "universe\\systems\\ku03\\bases\\ku03_04_base.ini",
                        "universe\\systems\\ku03\\bases\\ku03_05_base.ini",
                        "universe\\systems\\ku02\\bases\\ku02_01_base.ini",
                        "universe\\systems\\ku02\\bases\\ku02_02_base.ini",
                        "universe\\systems\\ku02\\bases\\ku02_03_base.ini",
                        "universe\\systems\\ku02\\bases\\ku02_04_base.ini",
                        "universe\\systems\\ku02\\bases\\ku02_05_base.ini",
                        "universe\\systems\\ku01\\bases\\ku01_01_base.ini",
                        "universe\\systems\\ku01\\bases\\ku01_02_base.ini",
                        "universe\\systems\\ku01\\bases\\ku01_03_base.ini",
                        "universe\\systems\\ku01\\bases\\ku01_04_base.ini",
                        "universe\\systems\\ku01\\bases\\ku01_05_base.ini",
                        "universe\\systems\\ku01\\bases\\ku01_06_base.ini",
                        "universe\\systems\\ku01\\bases\\ku01_07_base.ini",
                        "universe\\systems\\iw06\\bases\\iw06_01_base.ini",
                        "universe\\systems\\iw06\\bases\\iw06_02_base.ini",
                        "universe\\systems\\iw05\\bases\\iw05_01_base.ini",
                        "universe\\systems\\iw05\\bases\\iw05_02_base.ini",
                        "universe\\systems\\iw04\\bases\\iw04_01_base.ini",
                        "universe\\systems\\iw04\\bases\\iw04_02_base.ini",
                        "universe\\systems\\iw03\\bases\\iw03_01_base.ini",
                        "universe\\systems\\iw03\\bases\\iw03_02_base.ini",
                        "universe\\systems\\iw02\\bases\\iw02_01_base.ini",
                        "universe\\systems\\iw02\\bases\\iw02_02_base.ini",
                        "universe\\systems\\iw02\\bases\\iw02_03_base.ini",
                        "universe\\systems\\iw01\\bases\\iw01_01_base.ini",
                        "universe\\systems\\iw01\\bases\\iw01_02_base.ini",
                        "universe\\systems\\intro\\bases\\intro1_base.ini",
                        "universe\\systems\\intro\\bases\\intro2_base.ini",
                        "universe\\systems\\intro\\bases\\intro3_base.ini",
                        "universe\\systems\\hi02\\bases\\hi02_01_base.ini",
                        "universe\\systems\\hi02\\bases\\hi02_02_base.ini",
                        "universe\\systems\\hi01\\bases\\hi01_01_base.ini",
                        "universe\\systems\\ew06\\bases\\ew06_01_base.ini",
                        "universe\\systems\\ew06\\bases\\ew06_02_base.ini",
                        "universe\\systems\\ew04\\bases\\ew04_01_base.ini",
                        "universe\\systems\\ew03\\bases\\ew03_01_base.ini",
                        "universe\\systems\\ew03\\bases\\ew03_02_base.ini",
                        "universe\\systems\\ew02\\bases\\ew02_01_base.ini",
                        "universe\\systems\\ew01\\bases\\ew01_01_base.ini",
                        "universe\\systems\\ew01\\bases\\ew01_02_base.ini",
                        "universe\\systems\\bw10\\bases\\bw10_01_base.ini",
                        "universe\\systems\\bw10\\bases\\bw10_02_base.ini",
                        "universe\\systems\\bw09\\bases\\bw09_01_base.ini",
                        "universe\\systems\\bw09\\bases\\bw09_02_base.ini",
                        "universe\\systems\\bw09\\bases\\bw09_03_base.ini",
                        "universe\\systems\\bw08\\bases\\bw08_01_base.ini",
                        "universe\\systems\\bw08\\bases\\bw08_02_base.ini",
                        "universe\\systems\\bw08\\bases\\bw08_03_base.ini",
                        "universe\\systems\\bw07\\bases\\bw07_01_base.ini",
                        "universe\\systems\\bw07\\bases\\bw07_02_base.ini",
                        "universe\\systems\\bw06\\bases\\bw06_01_base.ini",
                        "universe\\systems\\bw06\\bases\\bw06_02_base.ini",
                        "universe\\systems\\bw05\\bases\\bw05_01_base.ini",
                        "universe\\systems\\bw05\\bases\\bw05_02_base.ini",
                        "universe\\systems\\bw05\\bases\\bw05_03_base.ini",
                        "universe\\systems\\bw04\\bases\\bw04_01_base.ini",
                        "universe\\systems\\bw04\\bases\\bw04_02_base.ini",
                        "universe\\systems\\bw03\\bases\\bw03_01_base.ini",
                        "universe\\systems\\bw03\\bases\\bw03_02_base.ini",
                        "universe\\systems\\bw03\\bases\\bw03_03_base.ini",
                        "universe\\systems\\bw02\\bases\\bw02_01_base.ini",
                        "universe\\systems\\bw02\\bases\\bw02_02_base.ini",
                        "universe\\systems\\bw01\\bases\\bw01_01_base.ini",
                        "universe\\systems\\bw01\\bases\\bw01_02_base.ini",
                        "universe\\systems\\bw01\\bases\\bw01_03_base.ini",
                        "universe\\systems\\bw01\\bases\\bw01_04_base.ini",
                        "universe\\systems\\bw01\\bases\\bw01_05_base.ini",
                        "universe\\systems\\br06\\bases\\br06_01_base.ini",
                        "universe\\systems\\br06\\bases\\br06_02_base.ini",
                        "universe\\systems\\br06\\bases\\br06_03_base.ini",
                        "universe\\systems\\br06\\bases\\br06_04_base.ini",
                        "universe\\systems\\br05\\bases\\br05_01_base.ini",
                        "universe\\systems\\br05\\bases\\br05_02_base.ini",
                        "universe\\systems\\br05\\bases\\br05_03_base.ini",
                        "universe\\systems\\br05\\bases\\br05_04_base.ini",
                        "universe\\systems\\br05\\bases\\br05_05_base.ini",
                        "universe\\systems\\br04\\bases\\br04_01_base.ini",
                        "universe\\systems\\br04\\bases\\br04_02_base.ini",
                        "universe\\systems\\br04\\bases\\br04_03_base.ini",
                        "universe\\systems\\br04\\bases\\br04_04_base.ini",
                        "universe\\systems\\br04\\bases\\br04_05_base.ini",
                        "universe\\systems\\br04\\bases\\br04_06_base.ini",
                        "universe\\systems\\br03\\bases\\br03_01_base.ini",
                        "universe\\systems\\br03\\bases\\br03_02_base.ini",
                        "universe\\systems\\br03\\bases\\br03_03_base.ini",
                        "universe\\systems\\br03\\bases\\br03_04_base.ini",
                        "universe\\systems\\br02\\bases\\br02_01_base.ini",
                        "universe\\systems\\br02\\bases\\br02_02_base.ini",
                        "universe\\systems\\br02\\bases\\br02_03_base.ini",
                        "universe\\systems\\br02\\bases\\br02_04_base.ini",
                        "universe\\systems\\br02\\bases\\br02_05_base.ini",
                        "universe\\systems\\br01\\bases\\br01_01_base.ini",
                        "universe\\systems\\br01\\bases\\br01_02_base.ini",
                        "universe\\systems\\br01\\bases\\br01_03_base.ini",
                        "universe\\systems\\br01\\bases\\br01_04_base.ini",
                        "universe\\systems\\br01\\bases\\br01_05_base.ini",
                        "universe\\systems\\br01\\bases\\br01_06_base.ini",
                        "universe\\systems\\br01\\bases\\br01_07_base.ini",
                        "universe\\systems\\br01\\bases\\br01_08_base.ini"
                    },

                // system base rooms
                new[]
                    {
                        "universe\\systems\\st03b\\bases\\rooms\\st03b_01_cityscape.ini",
                        "universe\\systems\\st02\\bases\\rooms\\st02_01_bar.ini",
                        "universe\\systems\\st02\\bases\\rooms\\st02_01_deck.ini",
                        "universe\\systems\\st02\\bases\\rooms\\st02_01_deck2.ini",
                        "universe\\systems\\st01\\bases\\rooms\\st01_01_bar.ini",
                        "universe\\systems\\st01\\bases\\rooms\\st01_01_equip.ini",
                        "universe\\systems\\st01\\bases\\rooms\\st01_01_lab.ini",
                        "universe\\systems\\st01\\bases\\rooms\\st01_01_planetscape.ini",
                        "universe\\systems\\st01\\bases\\rooms\\st01_01_shipdealer.ini",
                        "universe\\systems\\st01\\bases\\rooms\\st01_02_bar.ini",
                        "universe\\systems\\st01\\bases\\rooms\\st01_02_deck.ini",
                        "universe\\systems\\st01\\bases\\rooms\\st01_02_deck2.ini",
                        "universe\\systems\\rh05\\bases\\rooms\\rh05_01_bar.ini",
                        "universe\\systems\\rh05\\bases\\rooms\\rh05_01_deck.ini",
                        "universe\\systems\\rh05\\bases\\rooms\\rh05_02_bar.ini",
                        "universe\\systems\\rh05\\bases\\rooms\\rh05_02_deck.ini",
                        "universe\\systems\\rh05\\bases\\rooms\\rh05_03_bar.ini",
                        "universe\\systems\\rh05\\bases\\rooms\\rh05_03_deck.ini",
                        "universe\\systems\\rh05\\bases\\rooms\\rh05_04_bar.ini",
                        "universe\\systems\\rh05\\bases\\rooms\\rh05_04_cityscape.ini",
                        "universe\\systems\\rh05\\bases\\rooms\\rh05_04_equipment.ini",
                        "universe\\systems\\rh05\\bases\\rooms\\rh05_04_shipdealer.ini",
                        "universe\\systems\\rh05\\bases\\rooms\\rh05_04_trader.ini",
                        "universe\\systems\\rh04\\bases\\rooms\\rh04_01_bar.ini",
                        "universe\\systems\\rh04\\bases\\rooms\\rh04_01_planetscape.ini",
                        "universe\\systems\\rh04\\bases\\rooms\\rh04_01_planetscape2.ini",
                        "universe\\systems\\rh04\\bases\\rooms\\rh04_02_bar.ini",
                        "universe\\systems\\rh04\\bases\\rooms\\rh04_02_deck.ini",
                        "universe\\systems\\rh04\\bases\\rooms\\rh04_03_bar.ini",
                        "universe\\systems\\rh04\\bases\\rooms\\rh04_03_deck.ini",
                        "universe\\systems\\rh04\\bases\\rooms\\rh04_04_bar.ini",
                        "universe\\systems\\rh04\\bases\\rooms\\rh04_04_deck.ini",
                        "universe\\systems\\rh04\\bases\\rooms\\rh04_05_bar.ini",
                        "universe\\systems\\rh04\\bases\\rooms\\rh04_05_deck.ini",
                        "universe\\systems\\rh04\\bases\\rooms\\rh04_05_deck2.ini",
                        "universe\\systems\\rh04\\bases\\rooms\\rh04_05_shipdealer.ini",
                        "universe\\systems\\rh03\\bases\\rooms\\rh03_01_bar.ini",
                        "universe\\systems\\rh03\\bases\\rooms\\rh03_01_cityscape.ini",
                        "universe\\systems\\rh03\\bases\\rooms\\rh03_01_equipment.ini",
                        "universe\\systems\\rh03\\bases\\rooms\\rh03_01_shipdealer.ini",
                        "universe\\systems\\rh03\\bases\\rooms\\rh03_01_trader.ini",
                        "universe\\systems\\rh03\\bases\\rooms\\rh03_02_bar.ini",
                        "universe\\systems\\rh03\\bases\\rooms\\rh03_02_planetscape.ini",
                        "universe\\systems\\rh03\\bases\\rooms\\rh03_03_bar.ini",
                        "universe\\systems\\rh03\\bases\\rooms\\rh03_03_deck.ini",
                        "universe\\systems\\rh03\\bases\\rooms\\rh03_04_bar.ini",
                        "universe\\systems\\rh03\\bases\\rooms\\rh03_04_deck.ini",
                        "universe\\systems\\rh03\\bases\\rooms\\rh03_05_bar.ini",
                        "universe\\systems\\rh03\\bases\\rooms\\rh03_05_deck.ini",
                        "universe\\systems\\rh03\\bases\\rooms\\rh03_06_bar.ini",
                        "universe\\systems\\rh03\\bases\\rooms\\rh03_06_deck.ini",
                        "universe\\systems\\rh02\\bases\\rooms\\rh02_01_bar.ini",
                        "universe\\systems\\rh02\\bases\\rooms\\rh02_01_cityscape.ini",
                        "universe\\systems\\rh02\\bases\\rooms\\rh02_01_equipment.ini",
                        "universe\\systems\\rh02\\bases\\rooms\\rh02_01_shipdealer.ini",
                        "universe\\systems\\rh02\\bases\\rooms\\rh02_01_trader.ini",
                        "universe\\systems\\rh02\\bases\\rooms\\rh02_02_bar.ini",
                        "universe\\systems\\rh02\\bases\\rooms\\rh02_02_deck.ini",
                        "universe\\systems\\rh02\\bases\\rooms\\rh02_03_bar.ini",
                        "universe\\systems\\rh02\\bases\\rooms\\rh02_03_deck.ini",
                        "universe\\systems\\rh02\\bases\\rooms\\rh02_04_bar.ini",
                        "universe\\systems\\rh02\\bases\\rooms\\rh02_04_deck.ini",
                        "universe\\systems\\rh02\\bases\\rooms\\rh02_05_bar.ini",
                        "universe\\systems\\rh02\\bases\\rooms\\rh02_05_deck.ini",
                        "universe\\systems\\rh02\\bases\\rooms\\rh02_06_bar.ini",
                        "universe\\systems\\rh02\\bases\\rooms\\rh02_06_deck.ini",
                        "universe\\systems\\rh02\\bases\\rooms\\rh02_07_bar.ini",
                        "universe\\systems\\rh02\\bases\\rooms\\rh02_07_deck.ini",
                        "universe\\systems\\rh02\\bases\\rooms\\rh02_07_deck2.ini",
                        "universe\\systems\\rh01\\bases\\rooms\\rh01_01_bar.ini",
                        "universe\\systems\\rh01\\bases\\rooms\\rh01_01_cityscape.ini",
                        "universe\\systems\\rh01\\bases\\rooms\\rh01_01_equipment.ini",
                        "universe\\systems\\rh01\\bases\\rooms\\rh01_01_shipdealer.ini",
                        "universe\\systems\\rh01\\bases\\rooms\\rh01_01_trader.ini",
                        "universe\\systems\\rh01\\bases\\rooms\\rh01_02_bar.ini",
                        "universe\\systems\\rh01\\bases\\rooms\\rh01_02_deck.ini",
                        "universe\\systems\\rh01\\bases\\rooms\\rh01_03_bar.ini",
                        "universe\\systems\\rh01\\bases\\rooms\\rh01_03_deck.ini",
                        "universe\\systems\\rh01\\bases\\rooms\\rh01_04_bar.ini",
                        "universe\\systems\\rh01\\bases\\rooms\\rh01_04_deck.ini",
                        "universe\\systems\\rh01\\bases\\rooms\\rh01_05_bar.ini",
                        "universe\\systems\\rh01\\bases\\rooms\\rh01_05_deck.ini",
                        "universe\\systems\\rh01\\bases\\rooms\\rh01_06_bar.ini",
                        "universe\\systems\\rh01\\bases\\rooms\\rh01_06_deck.ini",
                        "universe\\systems\\rh01\\bases\\rooms\\rh01_07_bar.ini",
                        "universe\\systems\\rh01\\bases\\rooms\\rh01_07_deck.ini",
                        "universe\\systems\\rh01\\bases\\rooms\\rh01_08_bar.ini",
                        "universe\\systems\\rh01\\bases\\rooms\\rh01_08_deck.ini",
                        "universe\\systems\\li05\\bases\\rooms\\li05_01_deck.ini",
                        "universe\\systems\\li04\\bases\\rooms\\li04_01_bar.ini",
                        "universe\\systems\\li04\\bases\\rooms\\li04_01_cityscape.ini",
                        "universe\\systems\\li04\\bases\\rooms\\li04_01_equipment.ini",
                        "universe\\systems\\li04\\bases\\rooms\\li04_01_shipdealer.ini",
                        "universe\\systems\\li04\\bases\\rooms\\li04_01_trader.ini",
                        "universe\\systems\\li04\\bases\\rooms\\li04_02_bar.ini",
                        "universe\\systems\\li04\\bases\\rooms\\li04_02_deck.ini",
                        "universe\\systems\\li04\\bases\\rooms\\li04_03_bar.ini",
                        "universe\\systems\\li04\\bases\\rooms\\li04_03_deck.ini",
                        "universe\\systems\\li04\\bases\\rooms\\li04_04_bar.ini",
                        "universe\\systems\\li04\\bases\\rooms\\li04_04_deck.ini",
                        "universe\\systems\\li04\\bases\\rooms\\li04_05_bar.ini",
                        "universe\\systems\\li04\\bases\\rooms\\li04_05_deck.ini",
                        "universe\\systems\\li04\\bases\\rooms\\li04_06_bar.ini",
                        "universe\\systems\\li04\\bases\\rooms\\li04_06_deck.ini",
                        "universe\\systems\\li04\\bases\\rooms\\li04_06_deck02.ini",
                        "universe\\systems\\li03\\bases\\rooms\\li03_01_bar.ini",
                        "universe\\systems\\li03\\bases\\rooms\\li03_01_cityscape.ini",
                        "universe\\systems\\li03\\bases\\rooms\\li03_01_equipment.ini",
                        "universe\\systems\\li03\\bases\\rooms\\li03_01_shipdealer.ini",
                        "universe\\systems\\li03\\bases\\rooms\\li03_01_trader.ini",
                        "universe\\systems\\li03\\bases\\rooms\\li03_02_bar.ini",
                        "universe\\systems\\li03\\bases\\rooms\\li03_02_deck.ini",
                        "universe\\systems\\li03\\bases\\rooms\\li03_03_bar.ini",
                        "universe\\systems\\li03\\bases\\rooms\\li03_03_deck.ini",
                        "universe\\systems\\li03\\bases\\rooms\\li03_04_bar.ini",
                        "universe\\systems\\li03\\bases\\rooms\\li03_04_deck.ini",
                        "universe\\systems\\li02\\bases\\rooms\\li02_01_bar.ini",
                        "universe\\systems\\li02\\bases\\rooms\\li02_01_cityscape.ini",
                        "universe\\systems\\li02\\bases\\rooms\\li02_01_equipment.ini",
                        "universe\\systems\\li02\\bases\\rooms\\li02_01_shipdealer.ini",
                        "universe\\systems\\li02\\bases\\rooms\\li02_01_trader.ini",
                        "universe\\systems\\li02\\bases\\rooms\\li02_02_bar.ini",
                        "universe\\systems\\li02\\bases\\rooms\\li02_02_planetscape.ini",
                        "universe\\systems\\li02\\bases\\rooms\\li02_03_bar.ini",
                        "universe\\systems\\li02\\bases\\rooms\\li02_03_deck.ini",
                        "universe\\systems\\li02\\bases\\rooms\\li02_04_bar.ini",
                        "universe\\systems\\li02\\bases\\rooms\\li02_04_deck.ini",
                        "universe\\systems\\li02\\bases\\rooms\\li02_05_bar.ini",
                        "universe\\systems\\li02\\bases\\rooms\\li02_05_deck.ini",
                        "universe\\systems\\li02\\bases\\rooms\\li02_06_bar.ini",
                        "universe\\systems\\li02\\bases\\rooms\\li02_06_deck.ini",
                        "universe\\systems\\li02\\bases\\rooms\\li02_06_shipdealer.ini",
                        "universe\\systems\\li01\\bases\\rooms\\li01_01_bar.ini",
                        "universe\\systems\\li01\\bases\\rooms\\li01_01_cityscape.ini",
                        "universe\\systems\\li01\\bases\\rooms\\li01_01_equipment.ini",
                        "universe\\systems\\li01\\bases\\rooms\\li01_01_shipdealer.ini",
                        "universe\\systems\\li01\\bases\\rooms\\li01_01_trader.ini",
                        "universe\\systems\\li01\\bases\\rooms\\li01_02_bar.ini",
                        "universe\\systems\\li01\\bases\\rooms\\li01_02_planetscape.ini",
                        "universe\\systems\\li01\\bases\\rooms\\li01_02_planetscape2.ini",
                        "universe\\systems\\li01\\bases\\rooms\\li01_03_bar.ini",
                        "universe\\systems\\li01\\bases\\rooms\\li01_03_deck.ini",
                        "universe\\systems\\li01\\bases\\rooms\\li01_03_deck2.ini",
                        "universe\\systems\\li01\\bases\\rooms\\li01_04_bar.ini",
                        "universe\\systems\\li01\\bases\\rooms\\li01_04_deck.ini",
                        "universe\\systems\\li01\\bases\\rooms\\li01_05_bar.ini",
                        "universe\\systems\\li01\\bases\\rooms\\li01_05_deck.ini",
                        "universe\\systems\\li01\\bases\\rooms\\li01_06_bar.ini",
                        "universe\\systems\\li01\\bases\\rooms\\li01_06_deck.ini",
                        "universe\\systems\\li01\\bases\\rooms\\li01_07_bar.ini",
                        "universe\\systems\\li01\\bases\\rooms\\li01_07_deck.ini",
                        "universe\\systems\\li01\\bases\\rooms\\li01_08_bar.ini",
                        "universe\\systems\\li01\\bases\\rooms\\li01_08_deck.ini",
                        "universe\\systems\\li01\\bases\\rooms\\li01_09_bar.ini",
                        "universe\\systems\\li01\\bases\\rooms\\li01_09_deck.ini",
                        "universe\\systems\\li01\\bases\\rooms\\li01_10_bar.ini",
                        "universe\\systems\\li01\\bases\\rooms\\li01_10_deck.ini",
                        "universe\\systems\\li01\\bases\\rooms\\li01_11_bar.ini",
                        "universe\\systems\\li01\\bases\\rooms\\li01_11_deck.ini",
                        "universe\\systems\\li01\\bases\\rooms\\li01_12_bar.ini",
                        "universe\\systems\\li01\\bases\\rooms\\li01_12_deck.ini",
                        "universe\\systems\\li01\\bases\\rooms\\li01_12_deck2.ini",
                        "universe\\systems\\li01\\bases\\rooms\\li01_12_shipdealer.ini",
                        "universe\\systems\\li01\\bases\\rooms\\li01_13_bar.ini",
                        "universe\\systems\\li01\\bases\\rooms\\li01_13_deck.ini",
                        "universe\\systems\\li01\\bases\\rooms\\li01_13_shipdealer.ini",
                        "universe\\systems\\li01\\bases\\rooms\\li01_14_bar.ini",
                        "universe\\systems\\li01\\bases\\rooms\\li01_14_deck.ini",
                        "universe\\systems\\li01\\bases\\rooms\\li01_15_bar.ini",
                        "universe\\systems\\li01\\bases\\rooms\\li01_15_deck.ini",
                        "universe\\systems\\li01\\bases\\rooms\\li01_15_deck02.ini",
                        "universe\\systems\\ku07\\bases\\rooms\\ku07_01_bar.ini",
                        "universe\\systems\\ku07\\bases\\rooms\\ku07_01_deck.ini",
                        "universe\\systems\\ku07\\bases\\rooms\\ku07_02_bar.ini",
                        "universe\\systems\\ku07\\bases\\rooms\\ku07_02_deck.ini",
                        "universe\\systems\\ku06\\bases\\rooms\\ku06_01_bar.ini",
                        "universe\\systems\\ku06\\bases\\rooms\\ku06_01_cityscape.ini",
                        "universe\\systems\\ku06\\bases\\rooms\\ku06_01_equipment.ini",
                        "universe\\systems\\ku06\\bases\\rooms\\ku06_01_shipdealer.ini",
                        "universe\\systems\\ku06\\bases\\rooms\\ku06_01_trader.ini",
                        "universe\\systems\\ku05\\bases\\rooms\\ku05_01_bar.ini",
                        "universe\\systems\\ku05\\bases\\rooms\\ku05_01_deck.ini",
                        "universe\\systems\\ku05\\bases\\rooms\\ku05_02_bar.ini",
                        "universe\\systems\\ku05\\bases\\rooms\\ku05_02_deck.ini",
                        "universe\\systems\\ku05\\bases\\rooms\\ku05_03_bar.ini",
                        "universe\\systems\\ku05\\bases\\rooms\\ku05_03_deck.ini",
                        "universe\\systems\\ku05\\bases\\rooms\\ku05_04_bar.ini",
                        "universe\\systems\\ku05\\bases\\rooms\\ku05_04_deck.ini",
                        "universe\\systems\\ku04\\bases\\rooms\\ku04_01_bar.ini",
                        "universe\\systems\\ku04\\bases\\rooms\\ku04_01_cityscape.ini",
                        "universe\\systems\\ku04\\bases\\rooms\\ku04_01_equipment.ini",
                        "universe\\systems\\ku04\\bases\\rooms\\ku04_01_shipdealer.ini",
                        "universe\\systems\\ku04\\bases\\rooms\\ku04_01_trader.ini",
                        "universe\\systems\\ku04\\bases\\rooms\\ku04_02_bar.ini",
                        "universe\\systems\\ku04\\bases\\rooms\\ku04_02_deck.ini",
                        "universe\\systems\\ku04\\bases\\rooms\\ku04_03_bar.ini",
                        "universe\\systems\\ku04\\bases\\rooms\\ku04_03_deck.ini",
                        "universe\\systems\\ku04\\bases\\rooms\\ku04_04_bar.ini",
                        "universe\\systems\\ku04\\bases\\rooms\\ku04_04_deck.ini",
                        "universe\\systems\\ku04\\bases\\rooms\\ku04_05_bar.ini",
                        "universe\\systems\\ku04\\bases\\rooms\\ku04_05_deck.ini",
                        "universe\\systems\\ku04\\bases\\rooms\\ku04_06_bar.ini",
                        "universe\\systems\\ku04\\bases\\rooms\\ku04_06_deck.ini",
                        "universe\\systems\\ku03\\bases\\rooms\\ku03_01_bar.ini",
                        "universe\\systems\\ku03\\bases\\rooms\\ku03_01_cityscape.ini",
                        "universe\\systems\\ku03\\bases\\rooms\\ku03_01_equipment.ini",
                        "universe\\systems\\ku03\\bases\\rooms\\ku03_01_shipdealer.ini",
                        "universe\\systems\\ku03\\bases\\rooms\\ku03_01_trader.ini",
                        "universe\\systems\\ku03\\bases\\rooms\\ku03_02_bar.ini",
                        "universe\\systems\\ku03\\bases\\rooms\\ku03_02_deck.ini",
                        "universe\\systems\\ku03\\bases\\rooms\\ku03_03_bar.ini",
                        "universe\\systems\\ku03\\bases\\rooms\\ku03_03_deck.ini",
                        "universe\\systems\\ku03\\bases\\rooms\\ku03_04_bar.ini",
                        "universe\\systems\\ku03\\bases\\rooms\\ku03_04_deck.ini",
                        "universe\\systems\\ku03\\bases\\rooms\\ku03_05_bar.ini",
                        "universe\\systems\\ku03\\bases\\rooms\\ku03_05_deck.ini",
                        "universe\\systems\\ku02\\bases\\rooms\\ku02_01_bar.ini",
                        "universe\\systems\\ku02\\bases\\rooms\\ku02_01_deck.ini",
                        "universe\\systems\\ku02\\bases\\rooms\\ku02_02_bar.ini",
                        "universe\\systems\\ku02\\bases\\rooms\\ku02_02_deck.ini",
                        "universe\\systems\\ku02\\bases\\rooms\\ku02_03_bar.ini",
                        "universe\\systems\\ku02\\bases\\rooms\\ku02_03_deck.ini",
                        "universe\\systems\\ku02\\bases\\rooms\\ku02_04_bar.ini",
                        "universe\\systems\\ku02\\bases\\rooms\\ku02_04_planetscape.ini",
                        "universe\\systems\\ku02\\bases\\rooms\\ku02_05_bar.ini",
                        "universe\\systems\\ku02\\bases\\rooms\\ku02_05_deck.ini",
                        "universe\\systems\\ku01\\bases\\rooms\\ku01_01_bar.ini",
                        "universe\\systems\\ku01\\bases\\rooms\\ku01_01_cityscape.ini",
                        "universe\\systems\\ku01\\bases\\rooms\\ku01_01_equipment.ini",
                        "universe\\systems\\ku01\\bases\\rooms\\ku01_01_shipdealer.ini",
                        "universe\\systems\\ku01\\bases\\rooms\\ku01_01_trader.ini",
                        "universe\\systems\\ku01\\bases\\rooms\\ku01_02_bar.ini",
                        "universe\\systems\\ku01\\bases\\rooms\\ku01_02_deck.ini",
                        "universe\\systems\\ku01\\bases\\rooms\\ku01_03_bar.ini",
                        "universe\\systems\\ku01\\bases\\rooms\\ku01_03_deck.ini",
                        "universe\\systems\\ku01\\bases\\rooms\\ku01_04_bar.ini",
                        "universe\\systems\\ku01\\bases\\rooms\\ku01_04_deck.ini",
                        "universe\\systems\\ku01\\bases\\rooms\\ku01_05_bar.ini",
                        "universe\\systems\\ku01\\bases\\rooms\\ku01_05_deck.ini",
                        "universe\\systems\\ku01\\bases\\rooms\\ku01_06_bar.ini",
                        "universe\\systems\\ku01\\bases\\rooms\\ku01_06_deck.ini",
                        "universe\\systems\\ku01\\bases\\rooms\\ku01_07_bar.ini",
                        "universe\\systems\\ku01\\bases\\rooms\\ku01_07_deck.ini",
                        "universe\\systems\\iw06\\bases\\rooms\\iw06_01_bar.ini",
                        "universe\\systems\\iw06\\bases\\rooms\\iw06_01_deck.ini",
                        "universe\\systems\\iw06\\bases\\rooms\\iw06_02_bar.ini",
                        "universe\\systems\\iw06\\bases\\rooms\\iw06_02_deck.ini",
                        "universe\\systems\\iw05\\bases\\rooms\\iw05_01_bar.ini",
                        "universe\\systems\\iw05\\bases\\rooms\\iw05_01_deck.ini",
                        "universe\\systems\\iw05\\bases\\rooms\\iw05_02_bar.ini",
                        "universe\\systems\\iw05\\bases\\rooms\\iw05_02_deck.ini",
                        "universe\\systems\\iw04\\bases\\rooms\\iw04_01_bar.ini",
                        "universe\\systems\\iw04\\bases\\rooms\\iw04_01_planetscape.ini",
                        "universe\\systems\\iw04\\bases\\rooms\\iw04_02_bar.ini",
                        "universe\\systems\\iw04\\bases\\rooms\\iw04_02_deck.ini",
                        "universe\\systems\\iw04\\bases\\rooms\\iw04_02_shipdealer.ini",
                        "universe\\systems\\iw03\\bases\\rooms\\iw03_01_bar.ini",
                        "universe\\systems\\iw03\\bases\\rooms\\iw03_01_deck.ini",
                        "universe\\systems\\iw03\\bases\\rooms\\iw03_02_bar.ini",
                        "universe\\systems\\iw03\\bases\\rooms\\iw03_02_deck.ini",
                        "universe\\systems\\iw03\\bases\\rooms\\iw03_02_shipdealer.ini",
                        "universe\\systems\\iw02\\bases\\rooms\\iw02_01_bar.ini",
                        "universe\\systems\\iw02\\bases\\rooms\\iw02_01_deck.ini",
                        "universe\\systems\\iw02\\bases\\rooms\\iw02_02_bar.ini",
                        "universe\\systems\\iw02\\bases\\rooms\\iw02_02_deck.ini",
                        "universe\\systems\\iw02\\bases\\rooms\\iw02_03_bar.ini",
                        "universe\\systems\\iw02\\bases\\rooms\\iw02_03_deck.ini",
                        "universe\\systems\\iw02\\bases\\rooms\\iw02_03_deck02.ini",
                        "universe\\systems\\iw01\\bases\\rooms\\iw01_01_bar.ini",
                        "universe\\systems\\iw01\\bases\\rooms\\iw01_01_deck.ini",
                        "universe\\systems\\iw01\\bases\\rooms\\iw01_02_bar.ini",
                        "universe\\systems\\iw01\\bases\\rooms\\iw01_02_deck.ini",
                        "universe\\systems\\intro\\bases\\rooms\\intro1_cityscape.ini",
                        "universe\\systems\\intro\\bases\\rooms\\intro2_volcano.ini",
                        "universe\\systems\\intro\\bases\\rooms\\intro3_planetchunks.ini",
                        "universe\\systems\\hi02\\bases\\rooms\\hi02_01_bar.ini",
                        "universe\\systems\\hi02\\bases\\rooms\\hi02_01_cityscape.ini",
                        "universe\\systems\\hi02\\bases\\rooms\\hi02_01_equipment.ini",
                        "universe\\systems\\hi02\\bases\\rooms\\hi02_01_shipdealer.ini",
                        "universe\\systems\\hi02\\bases\\rooms\\hi02_01_trader.ini",
                        "universe\\systems\\hi02\\bases\\rooms\\hi02_02_bar.ini",
                        "universe\\systems\\hi02\\bases\\rooms\\hi02_02_deck.ini",
                        "universe\\systems\\hi01\\bases\\rooms\\hi01_01_bar.ini",
                        "universe\\systems\\hi01\\bases\\rooms\\hi01_01_cityscape.ini",
                        "universe\\systems\\hi01\\bases\\rooms\\hi01_01_equipment.ini",
                        "universe\\systems\\hi01\\bases\\rooms\\hi01_01_shipdealer.ini",
                        "universe\\systems\\hi01\\bases\\rooms\\hi01_01_trader.ini",
                        "universe\\systems\\ew06\\bases\\rooms\\ew06_01_bar.ini",
                        "universe\\systems\\ew06\\bases\\rooms\\ew06_01_cityscape.ini",
                        "universe\\systems\\ew06\\bases\\rooms\\ew06_01_equipment.ini",
                        "universe\\systems\\ew06\\bases\\rooms\\ew06_02_bar.ini",
                        "universe\\systems\\ew06\\bases\\rooms\\ew06_02_planetscape.ini",
                        "universe\\systems\\ew04\\bases\\rooms\\ew04_01_bar.ini",
                        "universe\\systems\\ew04\\bases\\rooms\\ew04_01_deck.ini",
                        "universe\\systems\\ew04\\bases\\rooms\\ew04_01_shipdealer.ini",
                        "universe\\systems\\ew03\\bases\\rooms\\ew03_01_bar.ini",
                        "universe\\systems\\ew03\\bases\\rooms\\ew03_01_deck.ini",
                        "universe\\systems\\ew03\\bases\\rooms\\ew03_01_shipdealer.ini",
                        "universe\\systems\\ew03\\bases\\rooms\\ew03_02_bar.ini",
                        "universe\\systems\\ew03\\bases\\rooms\\ew03_02_deck.ini",
                        "universe\\systems\\ew02\\bases\\rooms\\ew02_01_bar.ini",
                        "universe\\systems\\ew02\\bases\\rooms\\ew02_01_deck.ini",
                        "universe\\systems\\ew01\\bases\\rooms\\ew01_01_bar.ini",
                        "universe\\systems\\ew01\\bases\\rooms\\ew01_01_deck.ini",
                        "universe\\systems\\ew01\\bases\\rooms\\ew01_02_bar.ini",
                        "universe\\systems\\ew01\\bases\\rooms\\ew01_02_deck.ini",
                        "universe\\systems\\bw10\\bases\\rooms\\bw10_01_bar.ini",
                        "universe\\systems\\bw10\\bases\\rooms\\bw10_01_planetscape.ini",
                        "universe\\systems\\bw10\\bases\\rooms\\bw10_02_bar.ini",
                        "universe\\systems\\bw10\\bases\\rooms\\bw10_02_deck.ini",
                        "universe\\systems\\bw09\\bases\\rooms\\bw09_01_bar.ini",
                        "universe\\systems\\bw09\\bases\\rooms\\bw09_01_deck.ini",
                        "universe\\systems\\bw09\\bases\\rooms\\bw09_02_bar.ini",
                        "universe\\systems\\bw09\\bases\\rooms\\bw09_02_deck.ini",
                        "universe\\systems\\bw09\\bases\\rooms\\bw09_03_bar.ini",
                        "universe\\systems\\bw09\\bases\\rooms\\bw09_03_deck.ini",
                        "universe\\systems\\bw08\\bases\\rooms\\bw08_01_bar.ini",
                        "universe\\systems\\bw08\\bases\\rooms\\bw08_01_deck.ini",
                        "universe\\systems\\bw08\\bases\\rooms\\bw08_01_deck2.ini",
                        "universe\\systems\\bw08\\bases\\rooms\\bw08_02_bar.ini",
                        "universe\\systems\\bw08\\bases\\rooms\\bw08_02_deck.ini",
                        "universe\\systems\\bw08\\bases\\rooms\\bw08_03_bar.ini",
                        "universe\\systems\\bw08\\bases\\rooms\\bw08_03_deck.ini",
                        "universe\\systems\\bw07\\bases\\rooms\\bw07_01_bar.ini",
                        "universe\\systems\\bw07\\bases\\rooms\\bw07_01_deck.ini",
                        "universe\\systems\\bw07\\bases\\rooms\\bw07_02_bar.ini",
                        "universe\\systems\\bw07\\bases\\rooms\\bw07_02_deck.ini",
                        "universe\\systems\\bw06\\bases\\rooms\\bw06_01_bar.ini",
                        "universe\\systems\\bw06\\bases\\rooms\\bw06_01_planetscape.ini",
                        "universe\\systems\\bw06\\bases\\rooms\\bw06_02_bar.ini",
                        "universe\\systems\\bw06\\bases\\rooms\\bw06_02_deck.ini",
                        "universe\\systems\\bw05\\bases\\rooms\\bw05_01_bar.ini",
                        "universe\\systems\\bw05\\bases\\rooms\\bw05_01_deck.ini",
                        "universe\\systems\\bw05\\bases\\rooms\\bw05_02_bar.ini",
                        "universe\\systems\\bw05\\bases\\rooms\\bw05_02_deck.ini",
                        "universe\\systems\\bw05\\bases\\rooms\\bw05_03_bar.ini",
                        "universe\\systems\\bw05\\bases\\rooms\\bw05_03_deck.ini",
                        "universe\\systems\\bw04\\bases\\rooms\\bw04_01_bar.ini",
                        "universe\\systems\\bw04\\bases\\rooms\\bw04_01_deck.ini",
                        "universe\\systems\\bw04\\bases\\rooms\\bw04_02_bar.ini",
                        "universe\\systems\\bw04\\bases\\rooms\\bw04_02_deck.ini",
                        "universe\\systems\\bw03\\bases\\rooms\\bw03_01_bar.ini",
                        "universe\\systems\\bw03\\bases\\rooms\\bw03_01_deck.ini",
                        "universe\\systems\\bw03\\bases\\rooms\\bw03_01_shipdealer.ini",
                        "universe\\systems\\bw03\\bases\\rooms\\bw03_02_bar.ini",
                        "universe\\systems\\bw03\\bases\\rooms\\bw03_02_deck.ini",
                        "universe\\systems\\bw03\\bases\\rooms\\bw03_03_bar.ini",
                        "universe\\systems\\bw03\\bases\\rooms\\bw03_03_deck.ini",
                        "universe\\systems\\bw02\\bases\\rooms\\bw02_01_bar.ini",
                        "universe\\systems\\bw02\\bases\\rooms\\bw02_01_cityscape.ini",
                        "universe\\systems\\bw02\\bases\\rooms\\bw02_01_equipment.ini",
                        "universe\\systems\\bw02\\bases\\rooms\\bw02_01_shipdealer.ini",
                        "universe\\systems\\bw02\\bases\\rooms\\bw02_01_trader.ini",
                        "universe\\systems\\bw02\\bases\\rooms\\bw02_02_bar.ini",
                        "universe\\systems\\bw02\\bases\\rooms\\bw02_02_deck.ini",
                        "universe\\systems\\bw01\\bases\\rooms\\bw01_01_bar.ini",
                        "universe\\systems\\bw01\\bases\\rooms\\bw01_01_digsite.ini",
                        "universe\\systems\\bw01\\bases\\rooms\\bw01_01_planetscape.ini",
                        "universe\\systems\\bw01\\bases\\rooms\\bw01_01_planetscape2.ini",
                        "universe\\systems\\bw01\\bases\\rooms\\bw01_02_bar.ini",
                        "universe\\systems\\bw01\\bases\\rooms\\bw01_02_deck.ini",
                        "universe\\systems\\bw01\\bases\\rooms\\bw01_03_bar.ini",
                        "universe\\systems\\bw01\\bases\\rooms\\bw01_03_deck.ini",
                        "universe\\systems\\bw01\\bases\\rooms\\bw01_04_bar.ini",
                        "universe\\systems\\bw01\\bases\\rooms\\bw01_04_deck.ini",
                        "universe\\systems\\bw01\\bases\\rooms\\bw01_05_bar.ini",
                        "universe\\systems\\bw01\\bases\\rooms\\bw01_05_deck.ini",
                        "universe\\systems\\br06\\bases\\rooms\\br06_01_bar.ini",
                        "universe\\systems\\br06\\bases\\rooms\\br06_01_deck.ini",
                        "universe\\systems\\br06\\bases\\rooms\\br06_02_bar.ini",
                        "universe\\systems\\br06\\bases\\rooms\\br06_02_deck.ini",
                        "universe\\systems\\br06\\bases\\rooms\\br06_03_bar.ini",
                        "universe\\systems\\br06\\bases\\rooms\\br06_03_deck.ini",
                        "universe\\systems\\br06\\bases\\rooms\\br06_04_bar.ini",
                        "universe\\systems\\br06\\bases\\rooms\\br06_04_deck.ini",
                        "universe\\systems\\br05\\bases\\rooms\\br05_01_bar.ini",
                        "universe\\systems\\br05\\bases\\rooms\\br05_01_deck.ini",
                        "universe\\systems\\br05\\bases\\rooms\\br05_01_deck2.ini",
                        "universe\\systems\\br05\\bases\\rooms\\br05_02_bar.ini",
                        "universe\\systems\\br05\\bases\\rooms\\br05_02_deck.ini",
                        "universe\\systems\\br05\\bases\\rooms\\br05_02_deck2.ini",
                        "universe\\systems\\br05\\bases\\rooms\\br05_02_equipment.ini",
                        "universe\\systems\\br05\\bases\\rooms\\br05_03_bar.ini",
                        "universe\\systems\\br05\\bases\\rooms\\br05_03_deck.ini",
                        "universe\\systems\\br05\\bases\\rooms\\br05_04_bar.ini",
                        "universe\\systems\\br05\\bases\\rooms\\br05_04_deck.ini",
                        "universe\\systems\\br05\\bases\\rooms\\br05_05_bar.ini",
                        "universe\\systems\\br05\\bases\\rooms\\br05_05_deck.ini",
                        "universe\\systems\\br05\\bases\\rooms\\br05_05_shipdealer.ini",
                        "universe\\systems\\br04\\bases\\rooms\\br04_01_bar.ini",
                        "universe\\systems\\br04\\bases\\rooms\\br04_01_cityscape.ini",
                        "universe\\systems\\br04\\bases\\rooms\\br04_01_equipment.ini",
                        "universe\\systems\\br04\\bases\\rooms\\br04_01_shipdealer.ini",
                        "universe\\systems\\br04\\bases\\rooms\\br04_01_trader.ini",
                        "universe\\systems\\br04\\bases\\rooms\\br04_02_bar.ini",
                        "universe\\systems\\br04\\bases\\rooms\\br04_02_deck.ini",
                        "universe\\systems\\br04\\bases\\rooms\\br04_03_bar.ini",
                        "universe\\systems\\br04\\bases\\rooms\\br04_03_deck.ini",
                        "universe\\systems\\br04\\bases\\rooms\\br04_04_bar.ini",
                        "universe\\systems\\br04\\bases\\rooms\\br04_04_deck.ini",
                        "universe\\systems\\br04\\bases\\rooms\\br04_05_bar.ini",
                        "universe\\systems\\br04\\bases\\rooms\\br04_05_deck.ini",
                        "universe\\systems\\br04\\bases\\rooms\\br04_06_bar.ini",
                        "universe\\systems\\br04\\bases\\rooms\\br04_06_deck.ini",
                        "universe\\systems\\br03\\bases\\rooms\\br03_01_bar.ini",
                        "universe\\systems\\br03\\bases\\rooms\\br03_01_cityscape.ini",
                        "universe\\systems\\br03\\bases\\rooms\\br03_01_equipment.ini",
                        "universe\\systems\\br03\\bases\\rooms\\br03_01_shipdealer.ini",
                        "universe\\systems\\br03\\bases\\rooms\\br03_01_trader.ini",
                        "universe\\systems\\br03\\bases\\rooms\\br03_02_bar.ini",
                        "universe\\systems\\br03\\bases\\rooms\\br03_02_deck.ini",
                        "universe\\systems\\br03\\bases\\rooms\\br03_03_bar.ini",
                        "universe\\systems\\br03\\bases\\rooms\\br03_03_deck.ini",
                        "universe\\systems\\br03\\bases\\rooms\\br03_04_bar.ini",
                        "universe\\systems\\br03\\bases\\rooms\\br03_04_deck.ini",
                        "universe\\systems\\br02\\bases\\rooms\\br02_01_bar.ini",
                        "universe\\systems\\br02\\bases\\rooms\\br02_01_deck.ini",
                        "universe\\systems\\br02\\bases\\rooms\\br02_02_bar.ini",
                        "universe\\systems\\br02\\bases\\rooms\\br02_02_deck.ini",
                        "universe\\systems\\br02\\bases\\rooms\\br02_03_bar.ini",
                        "universe\\systems\\br02\\bases\\rooms\\br02_03_deck.ini",
                        "universe\\systems\\br02\\bases\\rooms\\br02_03_shipdealer.ini",
                        "universe\\systems\\br02\\bases\\rooms\\br02_04_bar.ini",
                        "universe\\systems\\br02\\bases\\rooms\\br02_04_deck.ini",
                        "universe\\systems\\br02\\bases\\rooms\\br02_05_bar.ini",
                        "universe\\systems\\br02\\bases\\rooms\\br02_05_deck.ini",
                        "universe\\systems\\br01\\bases\\rooms\\br01_01_bar.ini",
                        "universe\\systems\\br01\\bases\\rooms\\br01_01_cityscape.ini",
                        "universe\\systems\\br01\\bases\\rooms\\br01_01_equipment.ini",
                        "universe\\systems\\br01\\bases\\rooms\\br01_01_shipdealer.ini",
                        "universe\\systems\\br01\\bases\\rooms\\br01_01_trader.ini",
                        "universe\\systems\\br01\\bases\\rooms\\br01_02_bar.ini",
                        "universe\\systems\\br01\\bases\\rooms\\br01_02_deck.ini",
                        "universe\\systems\\br01\\bases\\rooms\\br01_03_bar.ini",
                        "universe\\systems\\br01\\bases\\rooms\\br01_03_deck.ini",
                        "universe\\systems\\br01\\bases\\rooms\\br01_04_bar.ini",
                        "universe\\systems\\br01\\bases\\rooms\\br01_04_deck.ini",
                        "universe\\systems\\br01\\bases\\rooms\\br01_05_bar.ini",
                        "universe\\systems\\br01\\bases\\rooms\\br01_05_deck.ini",
                        "universe\\systems\\br01\\bases\\rooms\\br01_06_bar.ini",
                        "universe\\systems\\br01\\bases\\rooms\\br01_06_deck.ini",
                        "universe\\systems\\br01\\bases\\rooms\\br01_07_bar.ini",
                        "universe\\systems\\br01\\bases\\rooms\\br01_07_deck.ini",
                        "universe\\systems\\br01\\bases\\rooms\\br01_08_bar.ini",
                        "universe\\systems\\br01\\bases\\rooms\\br01_08_deck.ini",
                        "universe\\systems\\br01\\bases\\rooms\\br01_08_shipdealer.ini"
                    },

                // shortest universe paths
                new[]
                    {
                        "universe\\systems_shortest_path.ini", "universe\\shortest_legal_path.ini",
                        "universe\\shortest_illegal_path.ini"
                    },

                // solar rings
                new[]
                    {
                        "solar\\rings\\aso.ini", "solar\\rings\\ew05_ring.ini", "solar\\rings\\ew05_ring2.ini",
                        "solar\\rings\\fuchu.ini", "solar\\rings\\ice.ini", "solar\\rings\\lava.ini",
                        "solar\\rings\\protoplanet.ini", "solar\\rings\\ross.ini", "solar\\rings\\weisser.ini"
                    },

                // solar nebulae
                new[]
                    {
                        "solar\\nebula\\badlands_li01.ini", "solar\\nebula\\br03_grasmere_ice_cloud.ini",
                        "solar\\nebula\\br03_keswick_ice_cloud.ini", "solar\\nebula\\br04_east_leeds_smog_cloud.ini",
                        "solar\\nebula\\br04_west_leeds_smog_cloud.ini", "solar\\nebula\\br06_arran_ice_cloud.ini",
                        "solar\\nebula\\br06_islay_ice_cloud.ini", "solar\\nebula\\bw01_graham_ice_cloud.ini",
                        "solar\\nebula\\bw03_walker_nebula.ini", "solar\\nebula\\bw05_crow_nebula.ini",
                        "solar\\nebula\\bw06_kunashir_edge_nebula.ini", "solar\\nebula\\bw07_donryu_edge_nebula.ini",
                        "solar\\nebula\\bw07_hiryu_crow_nebula.ini", "solar\\nebula\\crow_shapes.ini",
                        "solar\\nebula\\edge_shapes.ini", "solar\\nebula\\ew01_bermejo_barrier_cloud.ini",
                        "solar\\nebula\\ew01_malvinas_barrier_cloud.ini", "solar\\nebula\\ew02_edge_nebula.ini",
                        "solar\\nebula\\ew04_amarus_edge_nebula.ini", "solar\\nebula\\ew04_napo_edge_nebula.ini",
                        "solar\\nebula\\ew04_orinoco_edge_nebula.ini", "solar\\nebula\\generic_shapes.ini",
                        "solar\\nebula\\hi01_siniestre_edge_nebula.ini", "solar\\nebula\\hi02_gredos_walker_nebula.ini",
                        "solar\\nebula\\hi02_malvada_edge_nebula.ini", "solar\\nebula\\iw03_barrier_ice_cloud.ini",
                        "solar\\nebula\\iw04_paloma_cloud.ini", "solar\\nebula\\iw04_roatan_cloud.ini",
                        "solar\\nebula\\iw05_denko_cloud.ini", "solar\\nebula\\iw05_kuryo_cloud.ini",
                        "solar\\nebula\\iw05_matsuo_cloud.ini", "solar\\nebula\\iw06_komatsu_cloud.ini",
                        "solar\\nebula\\iw06_raiden_cloud.ini", "solar\\nebula\\iw06_reppu_cloud.ini",
                        "solar\\nebula\\ku02_keiun_cloud.ini", "solar\\nebula\\ku02_saiun_cloud.ini",
                        "solar\\nebula\\ku02_shiun_cloud.ini", "solar\\nebula\\ku03_seiran_cloud.ini",
                        "solar\\nebula\\ku04_chuyu_cloud.ini", "solar\\nebula\\ku04_hiryo_cloud.ini",
                        "solar\\nebula\\ku04_hiyo_cloud.ini", "solar\\nebula\\ku05_kayo_cloud.ini",
                        "solar\\nebula\\ku05_shiden_cloud.ini", "solar\\nebula\\ku05_unyo_cloud.ini",
                        "solar\\nebula\\ku06_okamura_cloud.ini", "solar\\nebula\\ku07_nampo_cloud.ini",
                        "solar\\nebula\\li01_badlands_nebula.ini",
                        "solar\\nebula\\li01_pittsburgh_pirate_base_nebula.ini",
                        "solar\\nebula\\li02_tahoe_ice_crystal_cloud.ini", "solar\\nebula\\li04_negra_nebula.ini",
                        "solar\\nebula\\li05_nebula.ini", "solar\\nebula\\rh03_ostneckarwolke_nebula.ini",
                        "solar\\nebula\\rh03_westneckarwolke_nebula.ini",
                        "solar\\nebula\\rh04_geheimniswolke_nebula.ini", "solar\\nebula\\rh05_daumann_wolke.ini",
                        "solar\\nebula\\rh05_schlectewolke.ini", "solar\\nebula\\rh05_schwefelwolke.ini",
                        "solar\\nebula\\st01_edge_nebula.ini", "solar\\nebula\\st02b_edge_nebula.ini",
                        "solar\\nebula\\st02_edge_nebula.ini", "solar\\nebula\\st03_edge_nebula.ini",
                        "solar\\nebula\\walker_shapes.ini", "solar\\nebula\\white_shapes.ini",
                        "solar\\nebula\\zone21_li01.ini"
                    },

                // solar blackholes
                new[] { "solar\\blackhole\\bh.ini", "solar\\blackhole\\bhshapes.ini", "solar\\blackhole\\omega13.ini" },

                // solar asteroids
                new[]
                    {
                        "solar\\asteroids\\br01_cornwall_rock_asteroid_field.ini",
                        "solar\\asteroids\\br01_cumbria_rock_asteroid_field.ini",
                        "solar\\asteroids\\br01_devon_rock_asteroid_field.ini", "solar\\asteroids\\br01_gas_pocket.ini",
                        "solar\\asteroids\\br01_somerset_rock_asteroid_field.ini",
                        "solar\\asteroids\\br01_southampton_debris_field.ini",
                        "solar\\asteroids\\br02_birmingham_ice_asteroid_field.ini",
                        "solar\\asteroids\\br02_newgate_minefield.ini",
                        "solar\\asteroids\\br02_sheffield_north_ice_asteroid_field.ini",
                        "solar\\asteroids\\br02_sheffield_south_ice_asteroid_field.ini",
                        "solar\\asteroids\\br03_cardiff_beryllium_field.ini",
                        "solar\\asteroids\\br03_cardiff_rock_asteroid_field.ini",
                        "solar\\asteroids\\br03_grasmere_ice_asteroid_field.ini",
                        "solar\\asteroids\\br03_keswick_ice_asteroid_field.ini",
                        "solar\\asteroids\\br03_newcastle_rock_asteroid_field.ini",
                        "solar\\asteroids\\br04_ld14_rock_asteroid_field.ini",
                        "solar\\asteroids\\br04_leeds_high_density_asteroids.ini",
                        "solar\\asteroids\\br04_leeds_smog_cloud_asteroids.ini",
                        "solar\\asteroids\\br04_radioactive_dust_field.ini",
                        "solar\\asteroids\\br04_stokes_mineable_asteroid_field.ini",
                        "solar\\asteroids\\br04_stokes_rock_asteroid_field.ini",
                        "solar\\asteroids\\br05_gsm_mineable_gold_field.ini",
                        "solar\\asteroids\\br05_gsm_minedout_field.ini",
                        "solar\\asteroids\\br05_hood_rock_asteroid_field.ini",
                        "solar\\asteroids\\br05_independent_gold_field.ini",
                        "solar\\asteroids\\br05_molly_gold_field.ini",
                        "solar\\asteroids\\br06_arran_ice_asteroid_field.ini",
                        "solar\\asteroids\\br06_islay_ice_asteroid_field.ini",
                        "solar\\asteroids\\br06_skye_ice_asteroid_field.ini",
                        "solar\\asteroids\\bw01_burgess_ice_crystal_field.ini",
                        "solar\\asteroids\\bw01_coombe_carbon_asteroid_field.ini",
                        "solar\\asteroids\\bw01_graham_ice_asteroid_field.ini",
                        "solar\\asteroids\\bw01_roth_carbon_asteroid_field.ini",
                        "solar\\asteroids\\bw01_wilkes_ice_asteroid_field.ini",
                        "solar\\asteroids\\bw02_ice_asteroid_field.ini", "solar\\asteroids\\bw02_minefield.ini",
                        "solar\\asteroids\\bw03_daumann_mineable.ini",
                        "solar\\asteroids\\bw03_ebersfelde_nonmineable.ini",
                        "solar\\asteroids\\bw03_ebers_gas_pocket.ini",
                        "solar\\asteroids\\bw03_furstenfelde_nonmineable.ini",
                        "solar\\asteroids\\bw03_fursten_gas_pocket.ini", "solar\\asteroids\\bw03_gubenfelde_lava.ini",
                        "solar\\asteroids\\bw03_guben_gas_pocket.ini", "solar\\asteroids\\bw03_img_mineable.ini",
                        "solar\\asteroids\\bw03_kruger_mineable.ini",
                        "solar\\asteroids\\bw04_nordostriemen_planetary_fragments.ini",
                        "solar\\asteroids\\bw04_nordwestriemen_planetary_fragments.ini",
                        "solar\\asteroids\\bw04_sudwestriemen_planetary_fragments.ini",
                        "solar\\asteroids\\bw04_von_rohe_lava_asteroids.ini",
                        "solar\\asteroids\\bw04_westriemen_planetary_fragments.ini",
                        "solar\\asteroids\\bw05_oxygen_pocket_field.ini",
                        "solar\\asteroids\\bw05_yanagi_debris_field.ini",
                        "solar\\asteroids\\bw05_yanagi_pocket_debris_field.ini",
                        "solar\\asteroids\\bw06_nemuro_dust_field.ini", "solar\\asteroids\\bw07_gikka_dust_field.ini",
                        "solar\\asteroids\\bw08_beryllium.ini", "solar\\asteroids\\bw08_mines.ini",
                        "solar\\asteroids\\bw08_niobium.ini", "solar\\asteroids\\bw08_rock_asteroid_field.ini",
                        "solar\\asteroids\\bw09_freeport6_ice_crystal_field.ini",
                        "solar\\asteroids\\bw09_nago_carbon_asteroid_field.ini",
                        "solar\\asteroids\\bw09_oxygen_ice_asteroid_field.ini",
                        "solar\\asteroids\\bw09_oxygen_pocket_field.ini",
                        "solar\\asteroids\\bw09_shinkaku_ice_asteroid_field.ini",
                        "solar\\asteroids\\bw10_ice_crystal_field.ini", "solar\\asteroids\\debris_shapes.ini",
                        "solar\\asteroids\\ew01_bermejo_ice_crystals.ini",
                        "solar\\asteroids\\ew01_malvinas_mineable_asteroids.ini",
                        "solar\\asteroids\\ew01_torres_ice_crystal_field.ini",
                        "solar\\asteroids\\ew02_nomad_asteroids.ini", "solar\\asteroids\\ew03_northeast_minefield.ini",
                        "solar\\asteroids\\ew03_planetary_fragments.ini", "solar\\asteroids\\ew03_west_minefield.ini",
                        "solar\\asteroids\\ew04_amarus_nomad_asteroids.ini",
                        "solar\\asteroids\\ew04_napo_nomad_asteroids.ini",
                        "solar\\asteroids\\ew04_orinoco_nomad_asteroids.ini",
                        "solar\\asteroids\\ew04_sabana_fragments.ini", "solar\\asteroids\\ew06_nomad_asteroids.ini",
                        "solar\\asteroids\\hi01_hispania_debris_field.ini",
                        "solar\\asteroids\\hi01_mahon_dust_field.ini",
                        "solar\\asteroids\\hi02_gredos_lava_asteroids.ini",
                        "solar\\asteroids\\hi02_malvada_nomad_asteroids.ini",
                        "solar\\asteroids\\hi02_tenereife_debris_field.ini",
                        "solar\\asteroids\\hi02_tenereife_rock_asteroids.ini", "solar\\asteroids\\ice_shapes.ini",
                        "solar\\asteroids\\iw01_tanner_rock_asteroid_field.ini",
                        "solar\\asteroids\\iw02_kenai_rock_asteroid_field.ini",
                        "solar\\asteroids\\iw02_sitka_rock_asteroid_field.ini",
                        "solar\\asteroids\\iw02_wrangell_rock_asteroid_field.ini",
                        "solar\\asteroids\\iw03_barrier_ice_asteroid_field.ini",
                        "solar\\asteroids\\iw03_weddell_ice_asteroid_field.ini",
                        "solar\\asteroids\\iw04_corcovado_ice_crystal_field.ini",
                        "solar\\asteroids\\iw04_paloma_ice_crystal_field.ini",
                        "solar\\asteroids\\iw04_roatan_ice_crystal_field.ini",
                        "solar\\asteroids\\iw05_denko_asteroids.ini", "solar\\asteroids\\iw05_kuryo_asteroids.ini",
                        "solar\\asteroids\\iw05_matsuo_asteroids.ini", "solar\\asteroids\\iw06_komatsu_asteroids.ini",
                        "solar\\asteroids\\iw06_raiden_asteroids.ini", "solar\\asteroids\\iw06_reppu_asteroids.ini",
                        "solar\\asteroids\\ku01_chiba_rock_asteroid_field.ini",
                        "solar\\asteroids\\ku01_kanto_rock_asteroid_field.ini",
                        "solar\\asteroids\\ku02_saiun_asteroids.ini", "solar\\asteroids\\ku03_hayate_dust_field.ini",
                        "solar\\asteroids\\ku03_ohka_dust_field.ini", "solar\\asteroids\\ku03_seiran_asteroids.ini",
                        "solar\\asteroids\\ku04_hiyo_asteroids.ini", "solar\\asteroids\\ku04_kansai_mine_field.ini",
                        "solar\\asteroids\\ku05_kayo_asteroids.ini",
                        "solar\\asteroids\\ku06_okamura_rock_asteroid_field.ini",
                        "solar\\asteroids\\ku07_izu_planetary_fragment_field.ini",
                        "solar\\asteroids\\ku07_nampo_planetary_fragment_field.ini",
                        "solar\\asteroids\\li01_badlands_asteroids.ini",
                        "solar\\asteroids\\li01_badlands_low_density_asteroids.ini",
                        "solar\\asteroids\\li01_debris_field_small.ini",
                        "solar\\asteroids\\li01_detroit_debris_field_001.ini",
                        "solar\\asteroids\\li01_detroit_high_density_debris.ini",
                        "solar\\asteroids\\li01_jersey_debris_field_001.ini",
                        "solar\\asteroids\\li01_pittsburgh_debris_field_001.ini",
                        "solar\\asteroids\\li01_pittsburgh_high_density_debris.ini",
                        "solar\\asteroids\\li01_zone21_mine_field.ini",
                        "solar\\asteroids\\li02_sierra_ice_asteroid_field.ini",
                        "solar\\asteroids\\li02_tahoe_ice_crystal_field.ini",
                        "solar\\asteroids\\li02_whitney_ice_asteroid_field.ini",
                        "solar\\asteroids\\li03_alamosa_rock_asteroid_field.ini",
                        "solar\\asteroids\\li03_cheyenne_rock_asteroid_field.ini",
                        "solar\\asteroids\\li03_copperton_rock_asteroid_field.ini",
                        "solar\\asteroids\\li03_silverton_rock_asteroid_field.ini",
                        "solar\\asteroids\\li04_east_dallas_debris_field.ini",
                        "solar\\asteroids\\li04_negra_cloud_debris.ini",
                        "solar\\asteroids\\li04_north_dallas_debris_field.ini",
                        "solar\\asteroids\\li04_south_dallas_debris_field.ini",
                        "solar\\asteroids\\li04_west_dallas_debris_field.ini",
                        "solar\\asteroids\\li05_ice_asteroids.ini", "solar\\asteroids\\mine_shapes.ini",
                        "solar\\asteroids\\rh01_aachen_minedout_asteroid_field.ini",
                        "solar\\asteroids\\rh01_harz_minedout_asteroid_field.ini",
                        "solar\\asteroids\\rh01_ruhr_debris_field.ini",
                        "solar\\asteroids\\rh01_ruhr_minedout_asteroid_field.ini",
                        "solar\\asteroids\\rh01_saar_minedout_asteroid_field.ini",
                        "solar\\asteroids\\rh02_alster_rock_asteroid_field.ini",
                        "solar\\asteroids\\rh02_kiel_rock_asteroid_field.ini",
                        "solar\\asteroids\\rh02_north_rock_asteroid_field.ini",
                        "solar\\asteroids\\rh02_south_rock_asteroid_field.ini",
                        "solar\\asteroids\\rh03_westnebel_asteroids.ini", "solar\\asteroids\\rh04_m10_mine_field.ini",
                        "solar\\asteroids\\rh04_taunus_field.ini", "solar\\asteroids\\rh04_verbotene_field.ini",
                        "solar\\asteroids\\rh05_daumann_feld.ini", "solar\\asteroids\\rh05_kruger_lavafeld.ini",
                        "solar\\asteroids\\rh05_leipzig_mine.ini", "solar\\asteroids\\rh05_schwefelwolke_lavafeld.ini",
                        "solar\\asteroids\\rh05_west_lavafeld.ini", "solar\\asteroids\\rock_shapes.ini",
                        "solar\\asteroids\\shapes.ini", "solar\\asteroids\\st01_nomad_asteroids.ini",
                        "solar\\asteroids\\st02_nomad_asteroids.ini", "solar\\asteroids\\st03b_nomad_asteroids.ini"
                    },

                // loadouts
                new[]
                    {
                        "ships\\loadouts.ini", "ships\\loadouts_special.ini", "ships\\loadouts_utility.ini",
                        "solar\\loadouts.ini"
                    },

                // audio sounds
                new[]
                    {
                        "audio\\ambience_sounds.ini", "audio\\engine_sounds.ini", "audio\\gf_sounds.ini",
                        "audio\\interface_sounds.ini", "audio\\music.ini", "audio\\sounds.ini",
                        "audio\\story_sounds.ini"
                    },

                // audio voices
                new[]
                    {
                        "audio\\voices_base_female.ini", "audio\\voices_base_male.ini", "audio\\voices_mission01.ini",
                        "audio\\voices_mission02.ini", "audio\\voices_mission03.ini", "audio\\voices_mission04.ini",
                        "audio\\voices_mission05.ini", "audio\\voices_mission06.ini", "audio\\voices_mission07.ini",
                        "audio\\voices_mission08.ini", "audio\\voices_mission09.ini", "audio\\voices_mission10.ini",
                        "audio\\voices_mission11.ini", "audio\\voices_mission12.ini", "audio\\voices_mission13.ini",
                        "audio\\voices_recognizable.ini", "audio\\voices_space_female.ini",
                        "audio\\voices_space_male.ini"
                    },

                // effects
                new[]
                    {
                        "fx\\effect_types.ini", "fx\\beam_effects.ini", "fx\\engines\\engines_ale.ini",
                        "fx\\equipment\\equipment_ale.ini", "fx\\explosions\\explosions_ale.ini",
                        "fx\\hull_hits\\hull_hits_ale.ini", "fx\\misc\\misc_ale.ini", "fx\\shields\\shields_ale.ini",
                        "fx\\space\\space_ale.ini", "fx\\weapons\\weapons_ale.ini", "fx\\effects.ini",
                        "fx\\effects_explosion.ini"
                    },

                // mission enounters
                new[]
                    {
                        "missions\\encounters\\area_armored_prisoner.ini", "missions\\encounters\\area_assault.ini",
                        "missions\\encounters\\area_bh_assault.ini", "missions\\encounters\\area_bh_defend.ini",
                        "missions\\encounters\\area_bh_scout.ini", "missions\\encounters\\area_defend.ini",
                        "missions\\encounters\\area_gunboats.ini", "missions\\encounters\\area_lifter.ini",
                        "missions\\encounters\\area_nomads.ini", "missions\\encounters\\area_nomads_m13.ini",
                        "missions\\encounters\\area_repair.ini", "missions\\encounters\\area_scavenger.ini",
                        "missions\\encounters\\area_scout.ini", "missions\\encounters\\area_trade_armored.ini",
                        "missions\\encounters\\area_trade_freighter.ini",
                        "missions\\encounters\\area_trade_freighter_pirate.ini",
                        "missions\\encounters\\area_trade_freighter_smuggler.ini",
                        "missions\\encounters\\area_trade_smuggler.ini", "missions\\encounters\\area_trade_trader.ini",
                        "missions\\encounters\\area_trade_transport.ini", "missions\\encounters\\miningp_scavenger.ini",
                        "missions\\encounters\\miningp_trade_transport.ini",
                        "missions\\encounters\\new_encounter_example.ini", "missions\\encounters\\patrolp_assault.ini",
                        "missions\\encounters\\patrolp_bh_assault.ini", "missions\\encounters\\patrolp_bh_patrol.ini",
                        "missions\\encounters\\patrolp_gov_patrol.ini", "missions\\encounters\\patrolp_gunboats.ini",
                        "missions\\encounters\\patrolp_pirate_patrol.ini",
                        "missions\\encounters\\tradelane_armored_prisoner.ini",
                        "missions\\encounters\\tradelane_trade_armored.ini",
                        "missions\\encounters\\tradelane_trade_freighter.ini",
                        "missions\\encounters\\tradelane_trade_transport.ini",
                        "missions\\encounters\\tradep_armored_prisoner.ini",
                        "missions\\encounters\\tradep_miner_transport.ini",
                        "missions\\encounters\\tradep_trade_armored.ini",
                        "missions\\encounters\\tradep_trade_freighter.ini",
                        "missions\\encounters\\tradep_trade_freighter_pirate.ini",
                        "missions\\encounters\\tradep_trade_freighter_smuggler.ini",
                        "missions\\encounters\\tradep_trade_smuggler.ini",
                        "missions\\encounters\\tradep_trade_trader.ini",
                        "missions\\encounters\\tradep_trade_transport.ini"
                    },

                // missions
                new[]
                    {
                        "missions\\m13\\m13.ini", "missions\\m12\\m12.ini", "missions\\m11\\m11.ini",
                        "missions\\m10\\m10.ini", "missions\\m09\\m09.ini", "missions\\m08\\m08.ini",
                        "missions\\m07\\m07.ini", "missions\\m06\\m06.ini", "missions\\m05\\m05.ini",
                        "missions\\m04\\m04.ini", "missions\\m03\\m03.ini", "missions\\m02\\m02.ini",
                        "missions\\m01b\\m01b.ini", "missions\\m01a\\m01a.ini"
                    },

                // mission ships
                new[]
                    {
                        "missions\\npcships.ini", "missions\\npcships_test.ini", "missions\\m13\\npcships.ini",
                        "missions\\m12\\npcships.ini", "missions\\m11\\npcships.ini", "missions\\m10\\npcships.ini",
                        "missions\\m09\\npcships.ini", "missions\\m08\\npcships.ini", "missions\\m07\\npcships.ini",
                        "missions\\m06\\npcships.ini", "missions\\m05\\npcships.ini", "missions\\m04\\npcships.ini",
                        "missions\\m03\\npcships.ini", "missions\\m02\\npcships.ini", "missions\\m01b\\npcships.ini",
                        "missions\\m01a\\npcships.ini"
                    },

                // mission nrmls
                new[]
                    {
                        "missions\\m13\\m013_endgame_st03b_nrml.ini", "missions\\m13\\m013_s072aa_st01_01_nrml.ini",
                        "missions\\m13\\m013_s072ab_st01_01_offer.ini", "missions\\m13\\m013_s072bb_st01_01_nrml.ini",
                        "missions\\m13\\m013_s072d_st01_01_reoffer.ini", "missions\\m13\\m013_s074x_st01_01_nrml.ini",
                        "missions\\m13\\m013_s075x_st01_02_nrml.ini", "missions\\m13\\m013_s076xa_li01_01_nrml.ini",
                        "missions\\m13\\m013_s076xb_li01_01_nrml.ini", "missions\\m12\\dummy_test.ini",
                        "missions\\m12\\m012_s071a_st02_01_offer.ini", "missions\\m12\\m012_s071d_st02_01_reoffer.ini",
                        "missions\\m11\\m011_s067a_iw02_03_offer.ini", "missions\\m11\\m011_s067d_iw02_03_reoffer.ini",
                        "missions\\m11\\m011_s068x_li01_12_nrml.ini", "missions\\m11\\m011_s069x_li05_01_nrml.ini",
                        "missions\\m11\\m011_s070x_li01_15_nrml.ini", "missions\\m10\\dummy_test.ini",
                        "missions\\m10\\m010_s055a_ku06_01_offer.ini", "missions\\m10\\m010_s055d_ku06_01_reoffer.ini",
                        "missions\\m10\\m010_s059x_rh01_01_nrml.ini", "missions\\m10\\m010_s062x_rh04_01_nrml.ini",
                        "missions\\m10\\m010_s063x_rh04_05_nrml.ini", "missions\\m10\\m010_s064x_rh02_07_nrml.ini",
                        "missions\\m09\\m009_s050a_ku06_01_bar_offer.ini",
                        "missions\\m09\\m009_s050d_ku06_01_bar_reoffer.ini",
                        "missions\\m09\\m009_s051xa_ku07_01_nrml.ini", "missions\\m09\\m009_s051xb_ku07_01_nrml.ini",
                        "missions\\m09\\m009_s051xc_ku07_01_nrml.ini", "missions\\m09\\m009_s052x_ku06_01_nrml.ini",
                        "missions\\m08\\m008_s045a_ku01_05_offer.ini", "missions\\m08\\m008_s045d_ku01_05_reoffer.ini",
                        "missions\\m08\\m008_s046x_ku06_01_nrml.ini", "missions\\m07\\m007_s037a_br04_01_offer.ini",
                        "missions\\m07\\m007_s037d_br04_01_reoffer.ini", "missions\\m07\\m007_s038xa_bw08_01_nrml.ini",
                        "missions\\m07\\m007_s038xb_bw08_01_nrml.ini", "missions\\m07\\m007_s039x_ku03_01_nrml.ini",
                        "missions\\m06\\m006_s032a_br05_01_offer.ini", "missions\\m06\\m006_s032d_br05_01_reoffer.ini",
                        "missions\\m06\\m006_s033x_br05_01_nrml.ini", "missions\\m06\\m006_s034x_br05_02_nrml.ini",
                        "missions\\m05\\m005_s026a_br03_01_offer.ini", "missions\\m05\\m005_s026d_br03_01_reoffer.ini",
                        "missions\\m05\\m005_s027xa_bw01_01_nrml.ini", "missions\\m05\\m005_s027xb_bw01_01_nrml.ini",
                        "missions\\m05\\m005_s027xc_bw01_01_nrml.ini", "missions\\m05\\m005_s028x_bw01_05_nrml.ini",
                        "missions\\m05\\m005_s029x_br04_01_nrml.ini", "missions\\m04\\m004_s019a_li01_01_offer.ini",
                        "missions\\m04\\m004_s019d_li01_01_reoffer.ini", "missions\\m04\\m004_s020x_li01_04_nrml.ini",
                        "missions\\m04\\m004_s021x_br04_01_nrml.ini", "missions\\m03\\m003_s012xa_li01_01_nrml.ini",
                        "missions\\m03\\m003_s012xb_li01_01_nrml.ini", "missions\\m03\\m003_s014a_li02_02_offer.ini",
                        "missions\\m03\\m003_s014d_li02_02_reoffer.ini", "missions\\m03\\m003_s015x_li02_02_nrml.ini",
                        "missions\\m02\\m002_s009a_li01_01_offer.ini", "missions\\m02\\m002_s009d_li01_01_reoffer.ini",
                        "missions\\m02\\m002_s010x_li01_01_nrml.ini", "missions\\m02\\m002_s011x_li01_03_nrml.ini",
                        "missions\\m01a\\m000_s002xe_li01_01_nrml.ini", "missions\\m01a\\m001a_s003x_li01_01_nrml.ini",
                        "missions\\m01a\\m001a_s004x_li01_01_nrml.ini", "missions\\m01a\\m001a_s005a_li01_01_offer.ini",
                        "missions\\m01a\\m001a_s005d_li01_01_reoffer.ini",
                        "missions\\m01a\\m001a_s006x_li01_02_nrml.ini"
                    },

                // equipment
                new[]
                    {
                        "equipment\\light_equip.ini", "equipment\\select_equip.ini", "equipment\\misc_equip.ini",
                        "equipment\\engine_equip.ini", "equipment\\st_equip.ini", "equipment\\weapon_equip.ini",
                        "equipment\\prop_equip.ini"
                    },

                // goods
                new[]
                    {
                        "equipment\\goods.ini", "equipment\\engine_good.ini", "equipment\\misc_good.ini",
                        "equipment\\st_good.ini", "equipment\\weapon_good.ini"
                    },

                // markets
                new[]
                    {
                        "equipment\\market_misc.ini", "equipment\\market_ships.ini", "equipment\\market_commodities.ini"
                    },

                // cockpits
                new[]
                    {
                        "cockpits\\rheinland\\r_elite.ini", "cockpits\\rheinland\\r_fighter.ini",
                        "cockpits\\rheinland\\r_freighter.ini", "cockpits\\liberty\\bh_elite.ini",
                        "cockpits\\liberty\\bh_elite2.ini", "cockpits\\liberty\\bh_fighter.ini",
                        "cockpits\\liberty\\l_elite.ini", "cockpits\\liberty\\l_fighter.ini",
                        "cockpits\\liberty\\l_freighter.ini", "cockpits\\liberty\\or_elite.ini",
                        "cockpits\\kusari\\k_elite.ini", "cockpits\\kusari\\k_fighter.ini",
                        "cockpits\\kusari\\k_freighter.ini", "cockpits\\kusari\\pi_elite.ini",
                        "cockpits\\kusari\\pi_elite2.ini", "cockpits\\kusari\\pi_fighter.ini",
                        "cockpits\\corsair\\bw_elite.ini", "cockpits\\corsair\\bw_elite2.ini",
                        "cockpits\\corsair\\bw_fighter.ini", "cockpits\\corsair\\bw_freighter.ini",
                        "cockpits\\corsair\\corsair.ini", "cockpits\\corsair\\corsair_elite.ini",
                        "cockpits\\corsair\\corsair_fighter.ini", "cockpits\\corsair\\corsair_freighter.ini",
                        "cockpits\\civilian\\civilian.ini", "cockpits\\civilian\\civilian2.ini",
                        "cockpits\\civilian\\civilian3.ini", "cockpits\\civilian\\civilian_elite.ini",
                        "cockpits\\civilian\\civilian_fighter.ini", "cockpits\\civilian\\civilian_vheavy.ini",
                        "cockpits\\bretonia\\b_elite.ini", "cockpits\\bretonia\\b_fighter.ini",
                        "cockpits\\bretonia\\b_freighter.ini"
                    },

                // ships
                new[] { "ships\\shiparch.ini", "ships\\rtc_shiparch.ini" },

                // pilots
                new[] { "missions\\pilots_population.ini", "missions\\pilots_story.ini" }
            };

        #endregion

        #region "Excluded Files"

        private static readonly string[] ExcludedFiles = {
            "concave.ini",
            "fx\\fuse_li_battleship.ini",
            "interface\\hud\\hudtarget.ini",
            "missions\\factionsets.ini",
            "missions\\mshipprops.ini",
            "missions\\encounters\\new_encounter_example.ini",
            "solar\\asteroids\\shapes.ini",
            "universe\\systems\\intro\\intro.ini"
        };

        #endregion

        private static int GetTemplateFileGroup(string filePath)
        {
            for (int i = 0; i < FileGroups.Length; ++i)
            {
                foreach (string sameFile in FileGroups[i])
                {
                    if (sameFile == filePath)
                    {
                        return i;
                    }
                }
            }

            return -1;
        }

        private static void CreateTemplateFromFile(string file, int dataPathIndex)
        {
            string filePath = file.Substring(dataPathIndex).ToLowerInvariant();
            if (Array.IndexOf(ExcludedFiles, filePath) != -1)
            {
                return;
            }

            IniDataTemplate newTemplate = new IniDataTemplate
                {
                    Path = filePath
                };
            BiniManager biniManager = new BiniManager(file);
            if (biniManager.Read())
            {
                newTemplate.Blocks = biniManager.Data;
            }
            else
            {
                IniManager inimanager = new IniManager(file);
                newTemplate.Blocks = inimanager.Read();
            }

            if (newTemplate.Blocks == null || newTemplate.Blocks.Count == 0)
            {
                return;
            }

            int selectedFileGroup = GetTemplateFileGroup(filePath);
            if (selectedFileGroup != -1)
            {
                List<string> selectedFileGroupData = new List<string>(FileGroups[selectedFileGroup]);
                foreach (IniDataTemplate template in DataList)
                {
                    if (selectedFileGroupData.Contains(template.Path.ToLowerInvariant()))
                    {
                        template.Blocks.AddRange(newTemplate.Blocks);
                        return;
                    }
                }
            }

            DataList.Add(newTemplate);
        }

        public static void TestUtfModels(string directory, bool include3db)
        {
            foreach (string file in Directory.GetFiles(directory))
            {
                string extension = Path.GetExtension(file);

                if (extension != null &&
                    (extension.Equals(".cmp", StringComparison.OrdinalIgnoreCase) ||
                     (include3db && extension.Equals(".3db", StringComparison.OrdinalIgnoreCase))))
                {
                    UtfModel.LoadModel(file);
                }
            }

            foreach (string subDirectory in Directory.GetDirectories(directory))
            {
                TestUtfModels(subDirectory, include3db);
            }
        }

        public static void ResaveFiles(string sourcePath, string targetPath)
        {
            DirectoryInfo directory = new DirectoryInfo(sourcePath);
            FileInfo[] files = directory.GetFiles("*.ini");

            if (files.Length > 0)
            {
                if (!Directory.Exists(targetPath))
                {
                    Directory.CreateDirectory(targetPath);
                }

                foreach (FileInfo file in files)
                {
                    ResaveFile(file.FullName, Path.Combine(targetPath, file.Name));
                }
            }

            foreach (DirectoryInfo subDirectory in directory.GetDirectories())
            {
                ResaveFiles(subDirectory.FullName, Path.Combine(targetPath, subDirectory.Name));
            }
        }

        private static void ResaveFile(string sourceFile, string targetFile)
        {
            int templateIndex = Helper.Template.Data.GetIndex(sourceFile);
            if (templateIndex == -1)
            {
                return;
            }

            FileManager fileManager = new FileManager(sourceFile)
                {
                    WriteEmptyLine = true,
                    WriteSpaces = true,
                };
            EditorIniData data = fileManager.Read(FileEncoding.Automatic, templateIndex);

            fileManager.File = targetFile;
            fileManager.Write(data);
        }
    }
}

#endif
