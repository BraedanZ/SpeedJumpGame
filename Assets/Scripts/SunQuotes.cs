using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SunQuotes : MonoBehaviour
{
    private int deathCount;
    private int madeJumps;
    private int happiness;

    private int numberOfStagedQuotes = 8;
    private string[] stagedQuotes;

    private int maxHappiness = 4;
    private int quotesPerHappiness = 4;
    private string[,] happinessQuotes;

    private int quoteIndex;

    private GameObject speechBubble;

    public TMP_Text quote;

    void Start() {

        speechBubble = this.gameObject.transform.GetChild(1).gameObject;
        // quote = speechBubble.transform.GetChild(0).gameObject;

        if (StaticClass.GetDifficulty() != 0) {
            happiness = 1;
        }

        deathCount = 0;
        madeJumps = 0;
        quoteIndex = 0;

        stagedQuotes = new string[numberOfStagedQuotes];
    
        // staged quotes
        stagedQuotes[0] = "Morning sunshine :)";
        stagedQuotes[1] = "Here's your first hard jump";
        stagedQuotes[2] = "Ah, I'm sure you'll get it next time!";
        stagedQuotes[3] = "Oh, you didn't.  Good thing you only went back 2 platforms.";
        stagedQuotes[4] = "Hmm, looks like every time you fall, the number of platforms you go back increases";
        stagedQuotes[5] = "Good thing it also seems like that number decreases after each successful jump";
        stagedQuotes[6] = "Here's another hard jump";
        stagedQuotes[7] = "Yikes, looks like all the hard jumps were lined up perfectly...";

        happinessQuotes = new string[maxHappiness, quotesPerHappiness];

        // happy quotes (casual)
        happinessQuotes[0, 0] = "YAY";
        happinessQuotes[0, 1] = "Good jump!";
        happinessQuotes[0, 2] = "EZ PZ";
        happinessQuotes[0, 3] = "You're gonna make it in no time!";

        // not mad quotes
        happinessQuotes[1, 0] = "Guess I'll go back over here";
        happinessQuotes[1, 1] = "Maybe next time";
        happinessQuotes[1, 2] = "Another miss";
        happinessQuotes[1, 3] = "Oops";

        // mad quotes
        happinessQuotes[2, 0] = "Okay, this is gonna be a long day I guess";
        happinessQuotes[2, 1] = "Really?";
        happinessQuotes[2, 2] = "You should really aim for the platform next time";
        happinessQuotes[2, 3] = "bro...";

        // pissed quotes
        happinessQuotes[3, 0] = "Will this day ever end?";
        happinessQuotes[3, 1] = "Yep, no suprise there";
        happinessQuotes[3, 2] = "WAAAAAAY BACK";
        happinessQuotes[3, 3] = "Feels like I've been here forever";
    }
    public void Died() {
        if(StaticClass.GetDifficulty() != 0) {
            deathCount++;
            if (deathCount == 1) {
                speechBubble.SetActive(false);
            }
            if (deathCount >= 5) {
                deathCount = 0;
                quote.text = happinessQuotes[happiness, quoteIndex];
                speechBubble.SetActive(true);
                quoteIndex++;
                if (quoteIndex == quotesPerHappiness) {
                    quoteIndex = 0;
                }
            }
        }
    }

    public void MadeJump() {
        if(StaticClass.GetDifficulty() == 0) {
            madeJumps++;
            if (madeJumps == 2) {
                speechBubble.SetActive(false);
            }
            if (madeJumps >= 10) {
                madeJumps = 0;
                string quote = happinessQuotes[0, quoteIndex];
                speechBubble.SetActive(true);
                quoteIndex++;
                if (quoteIndex > quotesPerHappiness) {
                    quoteIndex = 0;
                }
            }
        }
    }

    public void SetHappiness(int happiness) {
        if (this.happiness != happiness) {
            this.happiness = happiness;
            quoteIndex = 0;
        }
    }
}
