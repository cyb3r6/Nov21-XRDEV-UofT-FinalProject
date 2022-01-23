using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

// Game manager script for basic memory game
public class PuzzleManager : MonoBehaviour
{
    [SerializeReference] private List<GameObject> cubes;
    [SerializeField] private int totalBlinksInSequence = 3;

    // the random sequence of cube indexes lit up
    private List<int> sequence = new List<int>();

    // list of the player's guesses
    private List<int> guesses = new List<int>();

    // whether or not the user is eligible to start guessing
    private bool userInteractionTracked = false;

    private void Start()
    {
        StartCoroutine(LightUpCubes());
    }

    private void OnEnable()
    {
        // subscribe to the event
        foreach (GameObject cube in cubes)
        {
            cube.GetComponent<Cube>().OnCubeCollision += OnCubeSelected;
        }
    }

    private void OnDisable()
    {
        // unsubscribe from the event
        foreach (GameObject cube in cubes)
        {
            cube.GetComponent<Cube>().OnCubeCollision -= OnCubeSelected;
        }
    }

    private IEnumerator LightUpCubes()
    {
        for (int i = 0; i < totalBlinksInSequence; i++)
        {
            yield return new WaitForSeconds(2f);

            // randomly light up one of the cubes
            int randomIndex = Random.Range(0, cubes.Count);

            cubes[randomIndex].GetComponent<Cube>().LightUp();
            sequence.Add(randomIndex);

            yield return new WaitForSeconds(2f);
        }

        userInteractionTracked = true;
        yield return new WaitForEndOfFrame();
    }

    private void OnCubeSelected(GameObject cube)
    {
        // don't add guesses to the list until the light up sequence is completed
        if (userInteractionTracked)
        {
            guesses.Add(IndexOfCube(cube));
        }

        if (guesses.Count == sequence.Count && userInteractionTracked)
        {
            if (guesses.SequenceEqual(sequence))
            {
                Debug.Log("You Win!");
            } 
            else
            {
                Debug.Log("You Lose");
                // start sequence again
                Restart();
            }
        }
    }

    private int IndexOfCube(GameObject cube)
    {
        for (int i = 0; i < cubes.Count; i++)
        {
            if (cubes[i] == cube)
            {
                return i;
            }
        }
        return -99;
    }

    private void Restart()
    {
        sequence.Clear();
        guesses.Clear();
        userInteractionTracked = false;

        StartCoroutine(LightUpCubes());
    }
}
