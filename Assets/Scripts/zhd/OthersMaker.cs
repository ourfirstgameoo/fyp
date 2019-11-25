using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class OthersMaker : InfrastructureBehaviour
{
    public Material grassMaterial;
    public GameObject itemPrefeb;



    IEnumerator Start()
    {
        while (!map.IsReady)
        {
            yield return null;
        }

        foreach (var way in map.ways.FindAll((w) => { return w.wayType == OsmWay.WayType.Grass; }))
        {
            GameObject go = GameObject.Instantiate<GameObject>(itemPrefeb, this.transform);
            Vector3 localOrigin = GetCenter(way);
            go.transform.position = localOrigin - map.bounds.center;

            MeshFilter mf = go.GetComponent<MeshFilter>();
            MeshRenderer mr = go.GetComponent<MeshRenderer>();
            mr.material = grassMaterial;

            List<Vector3> vectors = new List<Vector3>();
            List<Vector3> normals = new List<Vector3>();
            List<int> indicies = new List<int>();

            //vectors.Add(Vector3.zero);
            //normals.Add(Vector3.up);
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

}
