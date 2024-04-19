using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mono.Data.Sqlite;
using System.Data;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    private float health;
    private float lerpTimer;
    public float maxHealth =100f;
    public float chipSpeed=2f;
    public Image frontHealthBar;
    public Image backHealthBar;
    long id = LoginAndRegister.id;
    private string db = "URI=file:OW3.db";
    System.Random rnd = new System.Random();
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        health=Mathf.Clamp(health,0,maxHealth);
        updateHealth();
        if(Input.GetKeyDown(KeyCode.A)) {
            TakeDamage(Random.Range(5, 10));
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            RestoreHealth(Random.Range(5, 10));
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(4);
            cratenewMatchHistory();
            
        }
    }
    public void updateHealth()
    {
        Debug.Log(health);
        float fillF = frontHealthBar.fillAmount;
        float fillB = backHealthBar.fillAmount;
        float hFraction = health / maxHealth;
        if(fillB>hFraction)
        {
            frontHealthBar.fillAmount = hFraction;
            backHealthBar.color = Color.red;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            backHealthBar.fillAmount=Mathf.Lerp(fillB,hFraction,percentComplete);
        }
        if(fillF <hFraction)
        {
            backHealthBar.color = Color.green;
            backHealthBar.fillAmount = hFraction;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            frontHealthBar.fillAmount = Mathf.Lerp(fillF, backHealthBar.fillAmount, percentComplete);
        }
    }
    public void TakeDamage(float damage)
    {
        health -= damage;
        lerpTimer = 0f;
    }
    public void RestoreHealth(float healthAmount)
    {
        health += healthAmount;
        lerpTimer= 0f;

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
                    command.CommandText = "UPDATE Users SET RANK = Users.RANK+20 WHERE ID =" + id + ";";
                    command.ExecuteNonQuery();
                    command.CommandText = "UPDATE MONEY  SET  MONEY = MONEY+20 WHERE ID =" + id + ";";
                    command.ExecuteNonQuery();
                }
                else
                {
                    command.CommandText = "UPDATE Users SET RANK = RANK-20 WHERE ID =" + id + ";";
                    command.ExecuteNonQuery();
                }

                command.Clone();
            }

        }
    }
}
