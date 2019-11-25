using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

class MapReader : MonoBehaviour
{
    [HideInInspector]
    public Dictionary<ulong, OsmNode> nodes;

    [HideInInspector]
    public List<OsmWay> ways;

    [HideInInspector]
    public OsmBounds bounds;

    public string resourceFilename;

    public bool IsReady { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        nodes = new Dictionary<ulong, OsmNode>();
        ways = new List<OsmWay>();
        //Load xml file(osm->xml-type )
        var txtAsset = Resources.Load<TextAsset>(resourceFilename);

        XmlDocument doc = new XmlDocument();
        doc.LoadXml(txtAsset.text);

        SetBounds(doc.SelectSingleNode("/osm/bounds"));
        GetNodes(doc.SelectNodes("/osm/node"));
        GetWays(doc.SelectNodes("/osm/way"));

        IsReady = true;


    }



    private void GetWays(XmlNodeList xmlNodeList)
    {
        foreach(XmlNode n in xmlNodeList)
        {
            OsmWay way = new OsmWay(n);
            ways.Add(way);
        }
    }

    private void GetNodes(XmlNodeList xmlNodeList)
    {
        foreach(XmlNode n in xmlNodeList)
        {
            OsmNode node = new OsmNode(n);
            nodes[node.ID] = node;
        }
    }

    private void SetBounds(XmlNode xmlNode)
    {
        bounds = new OsmBounds(xmlNode);
    }

    void Update()
    {
        //Debug.Log(bounds.center);
        foreach (OsmWay w in ways)
        {
            if (w.Visible)
            {
                Color c = Color.cyan;
                if (!w.IsBoundary) c = Color.red;

                for(int i = 1;i<w.NodeIDs.Count; i++)
                {
                    OsmNode p1 = nodes[w.NodeIDs[i - 1]];
                    OsmNode p2 = nodes[w.NodeIDs[i]];

                    Vector3 v1 = p1 - bounds.center;
                    Vector3 v2 = p2 - bounds.center;


                    Debug.DrawLine(v1, v2,c);

                }
            }
        }    
    }

}
