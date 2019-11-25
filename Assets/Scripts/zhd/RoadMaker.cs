using UnityEngine;
using System.Collections;
using System.Collections.Generic;

class RoadMaker : InfrastructureBehaviour
{

    public Material roadMaterial;
    public GameObject itemPrefeb;


    IEnumerator Start()
    {
        while (!map.IsReady)
        {
            yield return null;
        }
        #region renderring
        foreach (var way in map.ways.FindAll((w) => { return w.wayType == OsmWay.WayType.Road; }))
        {
            GameObject go = GameObject.Instantiate<GameObject>(itemPrefeb, this.transform);
            Vector3 localOrigin = GetCenter(way);
            go.transform.position = localOrigin - map.bounds.center;

            MeshFilter mf = go.GetComponent<MeshFilter>();
            MeshRenderer mr = go.GetComponent<MeshRenderer>();

            mr.material = roadMaterial;

            List<Vector3> vectors = new List<Vector3>();
            List<Vector3> normals = new List<Vector3>();
            List<int> indicies = new List<int>();
            
            for(int i  = 1; i < way.NodeIDs.Count; i++)
            {
                OsmNode p1 = map.nodes[way.NodeIDs[i - 1]];
                OsmNode p2 = map.nodes[way.NodeIDs[i]];

                Vector3 s1 = p1 - localOrigin;
                Vector3 s2 = p2 - localOrigin;

                Vector3 diff = (s2 - s1).normalized;
                s1 -= diff;
                s2 += diff;

                Vector3 v1 = s1 + Vector3.Cross(diff*1.5f, Vector3.up) ;
                Vector3 v2 = s1 - Vector3.Cross(diff * 1.5f, Vector3.up);
                Vector3 v3 = s2 + Vector3.Cross(diff * 1.5f, Vector3.up);
                Vector3 v4 = s2 - Vector3.Cross(diff * 1.5f, Vector3.up);

                vectors.Add(v1);
                vectors.Add(v2);
                vectors.Add(v3);
                vectors.Add(v4);

                normals.Add(Vector3.up);
                normals.Add(Vector3.up);
                normals.Add(Vector3.up);
                normals.Add(Vector3.up);

                int idx1, idx2, idx3, idx4;
                idx4 = vectors.Count - 1;
                idx3 = vectors.Count - 2;
                idx2 = vectors.Count - 3;
                idx1 = vectors.Count - 4;

                indicies.Add(idx1);
                indicies.Add(idx3);
                indicies.Add(idx2);

                indicies.Add(idx3);
                indicies.Add(idx4);
                indicies.Add(idx2);

            }
            mf.mesh.vertices = vectors.ToArray();
            mf.mesh.normals = normals.ToArray();
            mf.mesh.triangles = indicies.ToArray();

            yield return null;
        }
        #endregion

        
    }


}
