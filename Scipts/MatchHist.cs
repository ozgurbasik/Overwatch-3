using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MatchHist : MonoBehaviour
{
    private string dbName = "URI=file:OW3.db";
    public Text hist1;
    static long id=LoginAndRegister.id ;
   
    // Start is called before the first frame update
    void Start()
    {
        history();  
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void goback()
    {
        SceneManager.LoadScene(4);
    }

    public void avg()
    {
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT AVG(UserMatches.DMG)AS Avarage_DMG,AVG(UserMatches.Score) Avarage_Score ,AVG(UserMatches.Death) Avarage_Death FROM (SELECT * FROM MatchHistory \r\nINNER JOIN UsersToMatch ON MatchHistory.mID = UsersToMatch.mID\r\nINNER JOIN Users on UsersToMatch.ID = "+id+") as UserMatches;";
                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        string line = "Avarage DMG:" + reader["Avarage_DMG"] + " Avarage Score: " + reader["Avarage_Score"] + " Avarage Death:" + reader["Avarage_Death"];
                        hist1.text = line;

                       



                    }
                    reader.Close();
                }


            }
            connection.Close();
        }
    }

    private void history()
    {
        string s="";
        Debug.Log(id);
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM MatchHistory \r\nINNER JOIN UsersToMatch ON MatchHistory.mID = UsersToMatch.mID\r\nINNER JOIN Users on Users.ID = "+id+" ;";
                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        
                        string line = "MatchID:" + reader["mId"] + " DMG: " + reader["DMG"] + " Score:" + reader["Score"] + " Death:" + reader["Death"]  + " Nickname:" + reader["NICKNAME"] + " Rank: " + reader["RANK"]+"\n";
                        s += line;
                        



                    }
                    reader.Close();
                }
                
                
            }
            connection.Close();
        }
        hist1.text=s;
    }

}
