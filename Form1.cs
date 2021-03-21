using System;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using System.Collections.Generic;

namespace Teardown_SGE
{
    public partial class Form1 : Form
    {
        TextInfo textInfo = Thread.CurrentThread.CurrentCulture.TextInfo;
        string[] missions = {
            "mall_intro",
            "lee_computers"
        };

        public Form1()
        {
            InitializeComponent();
        }

        private decimal DecimalClamp(decimal value, decimal max, decimal min)
        {
            decimal result = value;
            if (value > max) result = max;
            if (value < min) result = min;
            return result;
        }

        private bool NodeExists(XmlNode elm)
        {
            return elm != null;
        }

        private bool IsToolEnabled(XmlNode tool)
        {
            if (tool?.HasChildNodes == true)
            {
                XmlNode child = tool.FirstChild;
                XmlAttribute att = child.Attributes["value"];
                if (att != null) return att.Value == "1";
            }
            return false;
        }

        private int GetLevelScore(XmlNode level)
        {
            XmlNode score;
            if (level?.SelectNodes("score").Count > 0)
            {
                score = level.SelectNodes("score")[0];
                if (NodeExists(score)) return int.Parse(score.Attributes["value"].Value);
            }
            return 0;
        }

        private decimal GetLevelTimeLeft(XmlNode level)
        {
            XmlNode timeleft;
            if (level?.SelectNodes("timeleft").Count > 0)
            {
                timeleft = level.SelectNodes("timeleft")[0];
                if (NodeExists(timeleft)) return new decimal(float.Parse(timeleft.Attributes["value"].Value));
            }
            return 0;
        }

        private decimal GetLevelMissionTime(XmlNode level)
        {
            XmlNode missiontime;
            if (level?.SelectNodes("missiontime").Count > 0)
            {
                missiontime = level.SelectNodes("missiontime")[0];
                if (NodeExists(missiontime)) return new decimal(float.Parse(missiontime.Attributes["value"].Value));
            }
            return 0;
        }

        private void SetNumericClamped(NumericUpDown updown, decimal val)
        {
            updown.Value = DecimalClamp(val, updown.Minimum, updown.Maximum);
        } 

        private Control GetControl(string name)
        {
            return this.Controls.Find(name, true)[0];
        }

        private void SetMission(XmlNode mission, string missionId)
        {
            XmlNode missionType = mission.SelectNodes(missionId)[0];
            SetNumericClamped((NumericUpDown)this.Controls.Find(missionId + "Score", true)[0], GetLevelScore(missionType));
            SetNumericClamped((NumericUpDown)this.Controls.Find(missionId + "TimeLeft", true)[0], GetLevelTimeLeft(missionType));
            SetNumericClamped((NumericUpDown)this.Controls.Find(missionId + "MissionTime", true)[0], GetLevelMissionTime(missionType));
        }

        private void LoadSaveData()
        {
            var fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Teardown\\savegame.xml");
            XmlDocument xmlDoc = new XmlDocument();
            using (StreamReader xmlSr = new StreamReader(fileName, true))
            {
                string unMalFormedXmlSr = Regex.Replace(xmlSr.ReadToEnd(), @"<(\d)", $@"<malformed$1"); // Help from Xorberax
                xmlDoc.Load(new MemoryStream(Encoding.UTF8.GetBytes(unMalFormedXmlSr)));
            }
            XmlNode registry;
            if (NodeExists(registry = xmlDoc.GetElementsByTagName("registry")[0])) savegameFileVersion.Text = registry.Attributes["version"].Value;
            if (IsToolEnabled(xmlDoc.GetElementsByTagName("sledge")[0])) sledgeCheckbox.Checked = true;
            if (IsToolEnabled(xmlDoc.GetElementsByTagName("spraycan")[0])) spraycanCheckbox.Checked = true;
            if (IsToolEnabled(xmlDoc.GetElementsByTagName("extinguisher")[0])) extinguisherCheckbox.Checked = true;
            if (IsToolEnabled(xmlDoc.GetElementsByTagName("blowtorch")[0])) blowtorchCheckbox.Checked = true;
            if (IsToolEnabled(xmlDoc.GetElementsByTagName("shotgun")[0])) shotgunCheckbox.Checked = true;
            if (IsToolEnabled(xmlDoc.GetElementsByTagName("plank")[0])) plankCheckbox.Checked = true;
            if (IsToolEnabled(xmlDoc.GetElementsByTagName("pipebomb")[0])) pipebombCheckbox.Checked = true;
            if (IsToolEnabled(xmlDoc.GetElementsByTagName("gun")[0])) gunCheckbox.Checked = true;
            if (IsToolEnabled(xmlDoc.GetElementsByTagName("bomb")[0])) bombCheckbox.Checked = true;
            if (IsToolEnabled(xmlDoc.GetElementsByTagName("rocket")[0])) launcherCheckbox.Checked = true;
            XmlNode mission;
            if (NodeExists(mission = xmlDoc.GetElementsByTagName("mission")[0]))
            {
                SetMission(mission, "mall_intro");
                SetMission(mission, "lee_computers");
                SetMission(mission, "lee_login");
                SetMission(mission, "mall_intro");
                SetMission(mission, "mall_intro");
                SetMission(mission, "mall_intro");

                XmlNode lee_computers = mission.SelectNodes("lee_computers")[0];
                SetNumericClamped(lee_computersScore, GetLevelScore(lee_computers));
                SetNumericClamped(lee_computersTimeLeft, GetLevelTimeLeft(lee_computers));
                SetNumericClamped(lee_computersMissionTime, GetLevelMissionTime(lee_computers));

                XmlNode lee_login = mission.SelectNodes("lee_login")[0];
                SetNumericClamped(lee_loginScore, GetLevelScore(lee_login));
                SetNumericClamped(lee_loginTimeLeft, GetLevelTimeLeft(lee_login));
                SetNumericClamped(lee_loginMissionTime, GetLevelMissionTime(lee_login));

                XmlNode marina_demolish = mission.SelectNodes("marina_demolish")[0];
                SetNumericClamped(marina_demolishScore, GetLevelScore(marina_demolish));
                SetNumericClamped(marina_demolishTimeLeft, GetLevelTimeLeft(marina_demolish));
                SetNumericClamped(marina_demolishMissionTime, GetLevelMissionTime(marina_demolish));

                XmlNode marina_cars = mission.SelectNodes("marina_cars")[0];
                SetNumericClamped(marina_carsScore, GetLevelScore(marina_cars));
                SetNumericClamped(marina_carsTimeLeft, GetLevelTimeLeft(marina_cars));
                SetNumericClamped(marina_carsMissionTime, GetLevelMissionTime(marina_cars));

                XmlNode marina_gps = mission.SelectNodes("marina_gps")[0];
                SetNumericClamped(marina_gpsScore, GetLevelScore(marina_gps));
                SetNumericClamped(marina_gpsTimeLeft, GetLevelTimeLeft(marina_gps));
                SetNumericClamped(marina_gpsMissionTime, GetLevelMissionTime(marina_gps));

                XmlNode mansion_pool = mission.SelectNodes("mansion_pool")[0];
                SetNumericClamped(mansion_poolScore, GetLevelScore(mansion_pool));
                SetNumericClamped(mansion_poolTimeLeft, GetLevelTimeLeft(mansion_pool));
                SetNumericClamped(mansion_poolMissionTime, GetLevelMissionTime(mansion_pool));

                XmlNode lee_safe = mission.SelectNodes("lee_safe")[0];
                SetNumericClamped(mansion_poolScore, GetLevelScore(lee_safe));
                SetNumericClamped(mansion_poolTimeLeft, GetLevelTimeLeft(lee_safe));
                SetNumericClamped(mansion_poolMissionTime, GetLevelMissionTime(lee_safe));

                XmlNode lee_tower = mission.SelectNodes("lee_tower")[0];
                SetNumericClamped(mansion_poolScore, GetLevelScore(lee_tower));
                SetNumericClamped(mansion_poolTimeLeft, GetLevelTimeLeft(lee_tower));
                SetNumericClamped(mansion_poolMissionTime, GetLevelMissionTime(lee_tower));

                XmlNode mansion_art = mission.SelectNodes("mansion_art")[0];
                SetNumericClamped(mansion_poolScore, GetLevelScore(mansion_art));
                SetNumericClamped(mansion_poolTimeLeft, GetLevelTimeLeft(mansion_art));
                SetNumericClamped(mansion_poolMissionTime, GetLevelMissionTime(mansion_art));

                XmlNode marina_tools = mission.SelectNodes("marina_tools")[0];
                SetNumericClamped(mansion_poolScore, GetLevelScore(marina_tools));
                SetNumericClamped(mansion_poolTimeLeft, GetLevelTimeLeft(marina_tools));
                SetNumericClamped(mansion_poolMissionTime, GetLevelMissionTime(marina_tools));

                XmlNode marina_art_back = mission.SelectNodes("marina_art_back")[0];
                SetNumericClamped(mansion_poolScore, GetLevelScore(marina_art_back));
                SetNumericClamped(mansion_poolTimeLeft, GetLevelTimeLeft(marina_art_back));
                SetNumericClamped(mansion_poolMissionTime, GetLevelMissionTime(marina_art_back));

                XmlNode mansion_fraud = mission.SelectNodes("mansion_fraud")[0];
                SetNumericClamped(mansion_poolScore, GetLevelScore(mansion_fraud));
                SetNumericClamped(mansion_poolTimeLeft, GetLevelTimeLeft(mansion_fraud));
                SetNumericClamped(mansion_poolMissionTime, GetLevelMissionTime(mansion_fraud));

                XmlNode caveisland_computers = mission.SelectNodes("caveisland_computers")[0];
                SetNumericClamped(mansion_poolScore, GetLevelScore(caveisland_computers));
                SetNumericClamped(mansion_poolTimeLeft, GetLevelTimeLeft(caveisland_computers));
                SetNumericClamped(mansion_poolMissionTime, GetLevelMissionTime(caveisland_computers));

                XmlNode mansion_race = mission.SelectNodes("mansion_race")[0];
                SetNumericClamped(mansion_poolScore, GetLevelScore(mansion_race));
                SetNumericClamped(mansion_poolTimeLeft, GetLevelTimeLeft(mansion_race));
                SetNumericClamped(mansion_poolMissionTime, GetLevelMissionTime(mansion_race));

                XmlNode mansion_safe = mission.SelectNodes("mansion_safe")[0];
                SetNumericClamped(mansion_poolScore, GetLevelScore(mansion_safe));
                SetNumericClamped(mansion_poolTimeLeft, GetLevelTimeLeft(mansion_safe));
                SetNumericClamped(mansion_poolMissionTime, GetLevelMissionTime(mansion_safe));

                XmlNode lee_powerplant = mission.SelectNodes("lee_powerplant")[0];
                SetNumericClamped(mansion_poolScore, GetLevelScore(lee_powerplant));
                SetNumericClamped(mansion_poolTimeLeft, GetLevelTimeLeft(lee_powerplant));
                SetNumericClamped(mansion_poolMissionTime, GetLevelMissionTime(lee_powerplant));

                XmlNode caveisland_propane = mission.SelectNodes("caveisland_propane")[0];
                SetNumericClamped(mansion_poolScore, GetLevelScore(caveisland_propane));
                SetNumericClamped(mansion_poolTimeLeft, GetLevelTimeLeft(caveisland_propane));
                SetNumericClamped(mansion_poolMissionTime, GetLevelMissionTime(caveisland_propane));

                XmlNode caveisland_dishes = mission.SelectNodes("caveisland_dishes")[0];
                SetNumericClamped(mansion_poolScore, GetLevelScore(caveisland_dishes));
                SetNumericClamped(mansion_poolTimeLeft, GetLevelTimeLeft(caveisland_dishes));
                SetNumericClamped(mansion_poolMissionTime, GetLevelMissionTime(caveisland_dishes));

                XmlNode lee_flooding = mission.SelectNodes("lee_flooding")[0];
                SetNumericClamped(mansion_poolScore, GetLevelScore(lee_flooding));
                SetNumericClamped(mansion_poolTimeLeft, GetLevelTimeLeft(lee_flooding));
                SetNumericClamped(mansion_poolMissionTime, GetLevelMissionTime(lee_flooding));

                XmlNode frustrum_chase = mission.SelectNodes("frustrum_chase")[0];
                SetNumericClamped(mansion_poolScore, GetLevelScore(frustrum_chase));
                SetNumericClamped(mansion_poolTimeLeft, GetLevelTimeLeft(frustrum_chase));
                SetNumericClamped(mansion_poolMissionTime, GetLevelMissionTime(frustrum_chase));
            }

            //int currentPosition = 0;
            //for (int i = 0; i < missions.Length; i++)
            //{
            //    currentPosition += 20;
            //    string curMission = missions[i];
            //    Label curMissionLabel = new Label();
            //    curMissionLabel.Parent = dynamicLevels;
            //    curMissionLabel.Text = "Test Text"; //textInfo.ToTitleCase(Regex.Replace(curMission, "_", " "));
            //    curMissionLabel.Location = new Point(3, 3);
            //    curMissionLabel.BringToFront();
            //    dynamicLevels.Controls.Add(curMissionLabel);
            //}

            XmlNode hub;
            if (NodeExists(hub = xmlDoc.GetElementsByTagName("hub")[0]))
            {
                XmlNode score;
                if (NodeExists(score = hub.SelectNodes("score")[0])) scoreNumeric.Value = int.Parse(score.Attributes["value"].Value);
                else scoreNumeric.Value = 0;
            }

            XmlNode cash;
            if (NodeExists(cash = xmlDoc.GetElementsByTagName("cash")[0])) cashNumeric.Value = int.Parse(cash.Attributes["value"].Value);
            else cashNumeric.Value = 0;

            XmlNode reward;
            if (NodeExists(reward = xmlDoc.GetElementsByTagName("reward")[0]))
            {
                if (NodeExists(reward.SelectNodes("malformed1000")[0])) rewardCheckbox1000.Checked = true;
                if (NodeExists(reward.SelectNodes("malformed2000")[0])) rewardCheckbox2000.Checked = true;
                if (NodeExists(reward.SelectNodes("malformed3000")[0])) rewardCheckbox3000.Checked = true;
                if (NodeExists(reward.SelectNodes("malformed4000")[0])) rewardCheckbox4000.Checked = true;
                if (NodeExists(reward.SelectNodes("malformed5000")[0])) rewardCheckbox5000.Checked = true;
            }
        }

        private XmlElement CreateToolElement(XmlDocument doc, string name)
        {
            XmlElement toolElm = doc.CreateElement(string.Empty, name, string.Empty);
            XmlElement enabled = doc.CreateElement(string.Empty, "enabled", string.Empty);
            enabled.SetAttribute("value", "1");
            toolElm.AppendChild(enabled);
            return toolElm;
        }

        private void SaveSaveData()
        {
            XmlDocument xmlDoc = new XmlDocument();
            XmlElement registry = xmlDoc.CreateElement(string.Empty, "registry", string.Empty);
            registry.SetAttribute("version", savegameFileVersion.Text);

            XmlElement savegame = xmlDoc.CreateElement(string.Empty, "savegame", string.Empty);
            registry.AppendChild(savegame);

            XmlElement tool = xmlDoc.CreateElement(string.Empty, "tool", string.Empty);
            savegame.AppendChild(tool);

            XmlElement message = xmlDoc.CreateElement(string.Empty, "message", string.Empty);
            savegame.AppendChild(message);

            XmlElement mission = xmlDoc.CreateElement(string.Empty, "mission", string.Empty);
            savegame.AppendChild(mission);

            XmlElement mall_intro = xmlDoc.CreateElement(string.Empty, "mall_intro", string.Empty);
            mission.AppendChild(mall_intro);
            XmlElement lee_computers = xmlDoc.CreateElement(string.Empty, "lee_computers", string.Empty);
            mission.AppendChild(lee_computers);
            XmlElement lee_login = xmlDoc.CreateElement(string.Empty, "lee_login", string.Empty);
            mission.AppendChild(lee_login);
            XmlElement marina_demolish = xmlDoc.CreateElement(string.Empty, "marina_demolish", string.Empty);
            mission.AppendChild(marina_demolish);
            XmlElement marina_cars = xmlDoc.CreateElement(string.Empty, "marina_cars", string.Empty);
            mission.AppendChild(marina_cars);
            XmlElement marina_gps = xmlDoc.CreateElement(string.Empty, "marina_gps", string.Empty);
            mission.AppendChild(marina_gps);
            XmlElement mansion_pool = xmlDoc.CreateElement(string.Empty, "mansion_pool", string.Empty);
            mission.AppendChild(mansion_pool);
            XmlElement lee_safe = xmlDoc.CreateElement(string.Empty, "lee_safe", string.Empty);
            mission.AppendChild(lee_safe);

            XmlElement hub = xmlDoc.CreateElement(string.Empty, "hub", string.Empty);
            savegame.AppendChild(hub);

            XmlElement score = xmlDoc.CreateElement(string.Empty, "score", string.Empty);
            score.SetAttribute("value", scoreNumeric.Value.ToString());
            hub.AppendChild(score);

            XmlElement valuable = xmlDoc.CreateElement(string.Empty, "valuable", string.Empty);
            savegame.AppendChild(valuable);

            XmlElement cash = xmlDoc.CreateElement(string.Empty, "cash", string.Empty);
            cash.SetAttribute("value", cashNumeric.Value.ToString());
            savegame.AppendChild(cash);

            if (sledgeCheckbox.Checked) tool.AppendChild(CreateToolElement(xmlDoc, "sledge"));
            if (spraycanCheckbox.Checked) tool.AppendChild(CreateToolElement(xmlDoc, "spraycan"));
            if (extinguisherCheckbox.Checked) tool.AppendChild(CreateToolElement(xmlDoc, "extinguisher"));
            if (blowtorchCheckbox.Checked) tool.AppendChild(CreateToolElement(xmlDoc, "blowtorch"));
            if (shotgunCheckbox.Checked) tool.AppendChild(CreateToolElement(xmlDoc, "shotgun"));
            if (plankCheckbox.Checked) tool.AppendChild(CreateToolElement(xmlDoc, "plank"));
            if (pipebombCheckbox.Checked) tool.AppendChild(CreateToolElement(xmlDoc, "pipebomb"));
            if (gunCheckbox.Checked) tool.AppendChild(CreateToolElement(xmlDoc, "gun"));
            if (bombCheckbox.Checked) tool.AppendChild(CreateToolElement(xmlDoc, "bomb"));
            if (launcherCheckbox.Checked) tool.AppendChild(CreateToolElement(xmlDoc, "rocket"));

            XmlElement reward = xmlDoc.CreateElement(string.Empty, "reward", string.Empty);
            if (rewardCheckbox1000.Checked)
            {
                XmlElement malformed1000 = xmlDoc.CreateElement(string.Empty, "malformed1000", string.Empty);
                malformed1000.SetAttribute("value", "1");
                reward.AppendChild(malformed1000);
            }
            if (rewardCheckbox2000.Checked)
            {
                XmlElement malformed2000 = xmlDoc.CreateElement(string.Empty, "malformed2000", string.Empty);
                malformed2000.SetAttribute("value", "1");
                reward.AppendChild(malformed2000);
            }
            if (rewardCheckbox3000.Checked)
            {
                XmlElement malformed3000 = xmlDoc.CreateElement(string.Empty, "malformed3000", string.Empty);
                malformed3000.SetAttribute("value", "1");
                reward.AppendChild(malformed3000);
            }
            if (rewardCheckbox4000.Checked)
            {
                XmlElement malformed4000 = xmlDoc.CreateElement(string.Empty, "malformed4000", string.Empty);
                malformed4000.SetAttribute("value", "1");
                reward.AppendChild(malformed4000);
            }
            if (rewardCheckbox5000.Checked)
            {
                XmlElement malformed5000 = xmlDoc.CreateElement(string.Empty, "malformed5000", string.Empty);
                malformed5000.SetAttribute("value", "1");
                reward.AppendChild(malformed5000);
            }
            savegame.AppendChild(reward);
            xmlDoc.AppendChild(registry);
            var fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Teardown\\savegame2.xml");
            using (StreamWriter xmlSw = new StreamWriter(fileName, false))
            {
                MemoryStream mStream = new MemoryStream();
                XmlTextWriter formatter = new XmlTextWriter(mStream, Encoding.UTF8);
                formatter.Formatting = Formatting.Indented;
                xmlDoc.WriteContentTo(formatter);
                formatter.Flush();
                mStream.Flush();
                mStream.Position = 0;
                StreamReader formatterReader = new StreamReader(mStream);
                string content = Regex.Replace(formatterReader.ReadToEnd(), @"<malformed", "<");
                Console.Write(content);
                xmlSw.Write(content);
            }
        }

        private void TogglePanel(Panel uiPanel)
        {
            generalSettingsPanel.Visible = false;
            toolSettingsPanel.Visible = false;
            levelSettingsPanel.Visible = false;
            challengeSettingsPanel.Visible = false;
            dynamicLevels.Visible = false;
            generalSettingsPanel.Enabled = false;
            toolSettingsPanel.Enabled = false;
            levelSettingsPanel.Enabled = false;
            challengeSettingsPanel.Enabled = false;
            dynamicLevels.Enabled = false;
            uiPanel.Visible = true;
            uiPanel.Enabled = true;
        }

        private void loadSavegameButton_Click(object sender, EventArgs e)
        {
            LoadSaveData();
        }

        private void saveSavegameButton_Click(object sender, EventArgs e)
        {
            SaveSaveData();
        }

        private void loadManuallyButton_Click(object sender, EventArgs e)
        {

        }

        private void saveManuallyButton_Click(object sender, EventArgs e)
        {

        }

        private void generalSettings_Click(object sender, EventArgs e)
        {
            TogglePanel(generalSettingsPanel);
        }

        private void toolSettings_Click(object sender, EventArgs e)
        {
            TogglePanel(toolSettingsPanel);
        }

        private void levelSettings_Click(object sender, EventArgs e)
        {
            TogglePanel(levelSettingsPanel);
        }

        private void challengeSettings_Click(object sender, EventArgs e)
        {
            TogglePanel(challengeSettingsPanel);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            TogglePanel(dynamicLevels);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://discord.gg/teardown");
        }

    }
}
