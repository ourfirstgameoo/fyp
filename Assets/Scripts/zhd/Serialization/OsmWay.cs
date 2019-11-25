using System.Collections.Generic;
using System.Xml;

class OsmWay : BaseOsm
{

    public enum WayType { Road, Building, Grass };

    public ulong ID { get; private set; }

    public bool Visible { get; private set; }

    public List<ulong> NodeIDs { get; private set; }

    public bool IsBoundary { get; private set; }
    public WayType wayType;
    public string region;
    public BuildingAttr attribute = new BuildingAttr();

    public OsmWay(XmlNode node)
    {
        NodeIDs = new List<ulong>();

        ID = GetAttribute<ulong>("id", node.Attributes);
        Visible = GetAttribute<bool>("visible", node.Attributes);

        XmlNodeList nds = node.SelectNodes("nd");
        foreach (XmlNode n in nds)
        {
            ulong refNo = GetAttribute<ulong>("ref", n.Attributes);
            NodeIDs.Add(refNo);
        }

        //determine the way type

        if (NodeIDs.Count > 1)
        {
            //because boundary is always circle
            IsBoundary = NodeIDs[0] == NodeIDs[NodeIDs.Count - 1];
        }

        XmlNodeList tags = node.SelectNodes("tag");

        foreach (XmlNode tag in tags)
        {
            string key = GetAttribute<string>("k", tag.Attributes);

            switch (key)
            {
                case "building:levels":
                    {
                        attribute.height = 3.0f * GetAttribute<float>("v", tag.Attributes);
                        break;
                    }
                case "height":
                    {
                        attribute.height = 0.3048f * GetAttribute<float>("v", tag.Attributes);
                        break;
                    }
                case "building":
                    {
                        wayType = WayType.Building;
                        switch (GetAttribute<string>("v", tag.Attributes))
                        {
                            case "yes":
                                {
                                    attribute.buildingType = BuildingAttr.BuildingType.others;
                                    break;
                                }
                            case "university":
                                {
                                    attribute.buildingType = BuildingAttr.BuildingType.uniBuilding;
                                    break;
                                }
                            case "dormitory":
                                {
                                    attribute.buildingType = BuildingAttr.BuildingType.dorBuilding;
                                    break;
                                }
                        }
                        break;
                    }
                case "name:zh":
                    {
                        attribute.name = GetAttribute<string>("v", tag.Attributes);
                        break;
                    }
                case "highway":
                    {
                        wayType = WayType.Road;
                        break;
                    }
                case "landuse":
                    {
                        if (GetAttribute<string>("v", tag.Attributes) == "grass")
                            wayType = WayType.Grass;
                        break;
                    }
                case "region":
                    {
                        attribute.setRegion(GetAttribute<string>("v", tag.Attributes));
                        break;
                    }
            }

        }

    }


}

