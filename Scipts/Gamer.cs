using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mono.Data.Sqlite;

public class Gamer : MonoBehaviour
{
    long id = LoginAndRegister.id;
    private string db = "URI=file:OW3.db";
    System.Random rnd = new System.Random();
    // Start is called before the first frame update
    void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            
            cratenewMatchHistory();
            SceneManager.LoadScene(4);
        }
    }
    void cratenewMatchHistory()
    {
        using (var connection = new SqliteConnection(db))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                int a = rnd.Next(1000, 10000);
                command.CommandText = "INSERT INTO MatchHistory (DMG,SCORE,DEATH) VALUES (" + a + "," + a / 1000 + "," + (10 - a / 1000) + ")";
                command.ExecuteNonQuery();
                long mId = 0;
                command.CommandText = "SELECT MAX(mID) AS mID FROM MatchHistory;";
                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        mId = (long)reader["mID"];
                    }
                    reader.Close();
                }
                command.CommandText = "INSERT INTO UsersToMatch VALUES (" + id + "," + mId + ");";
                command.ExecuteNonQuery();
                int winorlose = rnd.Next(0, 10);
                if (winorlose > 5)
                {
                    command.CommandText = "UPDATE Users SET  Users.RANK = Users.RANK+20 WHERE ID =" + id + ";";
                    command.ExecuteNonQuery();
                    command.CommandText = "UPDATE MONEY A SET  A.MONEY = A.MONEY+20 WHERE ID =" + id + ";";
                    command.ExecuteNonQuery();
                }
                else
                {
                    command.CommandText = "UPDATE Users SET Users.RANK = Users.RANK-20 WHERE ID =" + id + ";";
                    command.ExecuteNonQuery();
                }

                command.Clone();
            }

        }
    }
}
