using UnityEngine;
using System.Collections;
using System.Collections.Generic;


class BuildingMaker : InfrastructureBehaviour
{


    public Material building;
    public Material uniBuilding;
    public Material dorBuilding;

    public GameObject itemPrefeb;

    IEnumerator Start()
    {
        while (!map.IsReady)
        {
            yield return null;
        }
        foreach(var way in map.ways.FindAll((w) => { return w.wayType == OsmWay.WayType.Building && w.NodeIDs.Count > 1; }))
        {
            Vector3 localOrigin = GetCenter(way);

            GameObject go = GameObject.Instantiate<GameObject>(itemPrefeb,this.transform);
            go.layer = 10;
            go.transform.position =localOrigin - map.bounds.center;
            MeshFilter mf = go.GetComponent<MeshFilter>();
            MeshRenderer mr = go.GetComponent<MeshRenderer>();
            Building bl = go.GetComponent<Building>();
            
            //bc.size = new Vector3(Mathf.Abs((map.nodes[way.NodeIDs[1]] - localOrigin).x) * 2, bl.attr.height, Mathf.Abs((map.nodes[way.NodeIDs[1]] - localOrigin).z) * 2);
            //+ new Vector3(0,bl.attr.height,0)
            bl.attr = way.attribute;
            
            GameObject te = new GameObject();
            te.transform.SetParent(this.transform);
            TextMesh text = te.AddComponent<TextMesh>();
            te.transform.position = go.transform.position + new Vector3(0,bl.attr.height + 2,0);
            
            switch (bl.attr.buildingType)
            {
                case BuildingAttr.BuildingType.others:
                    {
                        mr.material = building;
                        break;
                    }
                case BuildingAttr.BuildingType.uniBuilding:
                    {
                        mr.material = uniBuilding;
                        break;
                    }
                case BuildingAttr.BuildingType.dorBuilding:
                    {
                        mr.material = dorBuilding;
                        break;
                    }
            }

            List < Vector3> vectors = new List<Vector3>();
            List<Vector3> normals = new List<Vector3>();
            List<int> indices = new List<int>();

            for (int i = 1; i < way.NodeIDs.Count; i++)
            {
                OsmNode p1 = map.nodes[way.NodeIDs[i - 1]];
                OsmNode p2 = map.nodes[way.NodeIDs[i]];

                Vector3 v1 = p1 - localOrigin;
                Vector3 v2 = p2 - localOrigin;
                Vector3 v3 = v1 + new Vector3(0, bl.attr.height, 0);
                Vector3 v4 = v2 + new Vector3(0, bl.attr.height, 0);

                vectors.Add(v1);
                vectors.Add(v2);
                vectors.Add(v3);
                vectors.Add(v4);

                normals.Add(-Vector3.forward);
                normals.Add(-Vector3.forward);
                normals.Add(-Vector3.forward);
                normals.Add(-Vector3.forward);

                int idx1, idx2, idx3, idx4;
                idx4 = vectors.Count - 1;
                idx3 = vectors.Count - 2;
                idx2 = vectors.Count - 3;
                idx1 = vectors.Count - 4;

                // first triangle v1, v3, v2
                indices.Add(idx1);
                indices.Add(idx3);
                indices.Add(idx2);

                // second         v3, v4, v2
                indices.Add(idx3);
                indices.Add(idx4);
                indices.Add(idx2);

                // third          v2, v3, v1
                indices.Add(idx2);
                indices.Add(idx3);
                indices.Add(idx1);

                // fourth         v2, v4, v3
                indices.Add(idx2);
                indices.Add(idx4);
                indices.Add(idx3);

                // And now the roof triangles
                indices.Add(0);
                indices.Add(idx3);
                indices.Add(idx4);

                // Don't forget the upside down one!
                indices.Add(idx4);
                indices.Add(idx3);
                indices.Add(0);

            }


            mf.mesh.vertices = vectors.ToArray();
            mf.mesh.normals = normals.ToArray();
            mf.mesh.triangles = indices.ToArray();
            MeshCollider mc = go.AddComponent<MeshCollider>();
            mc.sharedMesh = mf.mesh;
            text.text = bl.attr.name;
            text.fontSize = 28;
            yield return null;
        }
    }

}