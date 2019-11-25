using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class BuildingMaker2D : InfrastructureBehaviour
{

    public Material building;
    public Material uniBuilding;
    public Material dorBuilding;



    IEnumerator Start()
    {
        while (!map.IsReady)
        {
            yield return null;
        }
        foreach(var way in map.ways.FindAll((w) => { return ((int)w.wayType < 4 && (int)w.wayType > 0) && w.NodeIDs.Count > 1; }))
        {
            Vector3 localOrigin = GetCenter(way);

            GameObject go = new GameObject();

            go.transform.position = localOrigin - map.bounds.center;
            MeshFilter mf = go.AddComponent<MeshFilter>();
            MeshRenderer mr = go.AddComponent<MeshRenderer>();

            BuildingAttr attr = way.attribute;
            attr = way.attribute;
            GameObject te = new GameObject();
            TextMesh text = te.AddComponent<TextMesh>();
            te.transform.position = go.transform.position + new Vector3(0, attr.height + 2, 0);

            switch (attr.buildingType)
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

            List<Vector3> vectors = new List<Vector3>();
            List<Vector3> normals = new List<Vector3>();
            List<int> indicies = new List<int>();

            vectors.Add(Vector3.zero);
            normals.Add(Vector3.up);
            for (int i = 1; i < way.NodeIDs.Count; i++)
            {
                OsmNode p1 = map.nodes[way.NodeIDs[i - 1]];
                OsmNode p2 = map.nodes[way.NodeIDs[i]];

                Vector3 s1 = p1 - localOrigin;
                Vector3 s2 = p2 - localOrigin;

                

                vectors.Add(s1);
                vectors.Add(s2);
                

                normals.Add(Vector3.up);
                normals.Add(Vector3.up);

                int idx1, idx2;
                idx1 = vectors.Count - 1;
                idx2 = vectors.Count - 2;

                indicies.Add(idx1);
                indicies.Add(idx2);
                indicies.Add(0);



            }
            mf.mesh.vertices = vectors.ToArray();
            mf.mesh.normals = normals.ToArray();
            mf.mesh.triangles = indicies.ToArray();

            yield return null;
        }
    }

    // Update is called once per frame

}
