using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace M_DB 
{
    public abstract class DB<T> : ScriptableObject where T : IQueryableToDB
    {
        [SerializeField]
        protected List<T> list;                 // 중복 데이터 입력 가능
        protected Dictionary<uint, T> db;       // DB
        public List<T> GetList
        {
            get
            {
                return list.ToList();
            }
        }

        public bool TryGetItemData(uint ID, out T itemData)
        {
            if (db == null)
            {
                db = new Dictionary<uint, T>();
                list.ForEach(x => db.Add(x.ID, x));
            }

            return db.TryGetValue(ID, out itemData);
        }

    }

    public interface IQueryableToDB
    {
        public uint ID { get; }
    }

}

