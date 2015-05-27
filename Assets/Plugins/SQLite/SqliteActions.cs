using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SqliteActions : MonoBehaviour {

	public SqliteDatabase DataBase;

	public SqliteActions(string name)
	{
		DataBase = new SqliteDatabase (name);
	}

	public void CreateTable(string name, Dictionary<string, string> dico)
	{
		List<string> nameCol = new List<string> ();
		List<string> valueCol = new List<string> ();

		foreach (var element in dico.Keys)
		{
			nameCol.Add(element);
		}

		foreach (var element in dico.Values)
		{
			valueCol.Add(element);
		}

		DataBase.ExecuteQuery ("CREATE TABLE " + name + " ( ID int )");

		for (int i = 0; i < nameCol.Count; i++)
		{
			DataBase.ExecuteQuery ("ALTER TABLE " + name + " Add " + nameCol[i] + " " + valueCol[i]);
		}
	}

	public void Insert(string name, Dictionary<string, string> dico)
	{
		int id = 0;
		List<string> nameCol = new List<string> ();
		List<string> valueCol = new List<string> ();
		
		foreach (var element in dico.Keys)
		{
			nameCol.Add(element);
		}
		
		foreach (var element in dico.Values)
		{
			valueCol.Add(element);
		}

		DataTable data = DataBase.ExecuteQuery ("select ID from " + name + " order by id desc limit 1");
		object idH;
		foreach (var value in data.Rows)
		{
			if (value.TryGetValue ("ID", out idH))
			{
				id = (int)idH + 1;
				Debug.Log("je passe");
			}
		}

		DataBase.ExecuteQuery ("INSERT INTO " + name + " (ID) VALUES ('" + id + "')");
		for (int i = 0; i < nameCol.Count; i++)
			DataBase.ExecuteQuery ("UPDATE " + name + " SET " + nameCol[i] + " = '" + valueCol[i] + "' WHERE ID = '" + id + "'");
	}
}
