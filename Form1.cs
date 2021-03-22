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
            if (updown != null) updown.Value = DecimalClamp(val, updown.Minimum, updown.Maximum);
            else return;
        } 

        private Control GetControl(string name)
        {
            if (Controls.Find(name, true).Length > 0) return Controls.Find(name, true)[0];
            return null;
        }

        private void SetMission(XmlNode mission, string missionId)
        {
            XmlNode missionType = mission.SelectNodes(missionId)[0];
            SetNumericClamped((NumericUpDown)GetControl(missionId + "Score"), GetLevelScore(missionType));
            SetNumericClamped((NumericUpDown)GetControl(missionId + "TimeLeft"), GetLevelTimeLeft(missionType));
            SetNumericClamped((NumericUpDown)GetControl(missionId + "MissionTime"), GetLevelMissionTime(missionType));
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
                SetMission(mission, "marina_demolish");
                SetMission(mission, "marina_cars");
                SetMission(mission, "marina_gps");
                SetMission(mission, "mansion_pool");
                SetMission(mission, "lee_safe");
                SetMission(mission, "lee_tower");
                SetMission(mission, "mansion_art");
                SetMission(mission, "marina_tools");
                SetMission(mission, "marina_art_back");
                SetMission(mission, "mansion_fraud");
                SetMission(mission, "caveisland_computers");
                SetMission(mission, "mansion_race");
                SetMission(mission, "mansion_safe");
                SetMission(mission, "lee_powerplant");
                SetMission(mission, "caveisland_propane");
                SetMission(mission, "caveisland_dishes");
                SetMission(mission, "lee_flooding");
                SetMission(mission, "frustrum_chase");
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
                if (NodeExists(score = hub.SelectNodes("score")[0])) SetNumericClamped(scoreNumeric, new decimal(float.Parse(score.Attributes["value"].Value)));
                else scoreNumeric.Value = 0;
            }
            
            XmlNode cash;
            if (NodeExists(cash = xmlDoc.GetElementsByTagName("cash")[0])) SetNumericClamped(cashNumeric, new decimal(float.Parse(cash.Attributes["value"].Value)));
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

        private void CreateMissionElement(XmlElement mission, XmlDocument doc, string missionId)
        {
            CheckBox missionCheck = (CheckBox)GetControl(missionId + "Enabled");
            NumericUpDown score = (NumericUpDown)GetControl(missionId + "Score");
            NumericUpDown timeLeft = (NumericUpDown)GetControl(missionId + "TimeLeft");
            NumericUpDown missionTime = (NumericUpDown)GetControl(missionId + "MissionTime");
            XmlElement scoreElement = doc.CreateElement(string.Empty, "score", string.Empty);
            XmlElement leftElement = doc.CreateElement(string.Empty, "timeleft", string.Empty);
            XmlElement timeElement = doc.CreateElement(string.Empty, "missiontime", string.Empty);
            if (missionCheck.Checked)
            {
                scoreElement.SetAttribute("value", score.Value.ToString());
                leftElement.SetAttribute("value", timeLeft.Value.ToString());
                timeElement.SetAttribute("value", missionTime.Value.ToString());
            }   
            else return;
            XmlElement missionType = doc.CreateElement(string.Empty, missionId, string.Empty);
            missionType.AppendChild(scoreElement);
            missionType.AppendChild(leftElement);
            missionType.AppendChild(timeElement);
            mission.AppendChild(missionType);
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

            CreateMissionElement(mission, xmlDoc, "mall_intro");
            CreateMissionElement(mission, xmlDoc, "lee_computers");
            CreateMissionElement(mission, xmlDoc, "lee_login");
            CreateMissionElement(mission, xmlDoc, "marina_demolish");
            CreateMissionElement(mission, xmlDoc, "marina_cars");
            CreateMissionElement(mission, xmlDoc, "marina_gps");
            CreateMissionElement(mission, xmlDoc, "mansion_pool");
            CreateMissionElement(mission, xmlDoc, "lee_safe");
            CreateMissionElement(mission, xmlDoc, "lee_tower");
            CreateMissionElement(mission, xmlDoc, "mansion_art");
            CreateMissionElement(mission, xmlDoc, "marina_art_back");
            CreateMissionElement(mission, xmlDoc, "marina_tools");
            CreateMissionElement(mission, xmlDoc, "mansion_fraud");
            CreateMissionElement(mission, xmlDoc, "caveisland_computers");
            CreateMissionElement(mission, xmlDoc, "mansion_safe");
            CreateMissionElement(mission, xmlDoc, "mansion_race");
            CreateMissionElement(mission, xmlDoc, "lee_powerplant");
            CreateMissionElement(mission, xmlDoc, "caveisland_propane");
            CreateMissionElement(mission, xmlDoc, "caveisland_dishes");
            CreateMissionElement(mission, xmlDoc, "lee_flooding");
            CreateMissionElement(mission, xmlDoc, "frustrum_chase");

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
            var fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Teardown\\savegame.xml");
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
