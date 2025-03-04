using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Required for UI Elements

public class EnvironmentalImpactGame : MonoBehaviour
{
    // Resources for managing the community
    public int water = 1000;
    public int energy = 1000;
    public int food = 1000;
    public int sustainabilityPoints = 0;

    // UI Elements
    public Text waterText;
    public Text energyText;
    public Text foodText;
    public Text sustainabilityPointsText;
    public Text disasterAlertText;

    // Disaster probabilities
    public float earthquakeProbability = 0.01f;
    public float floodProbability = 0.02f;
    public float wildfireProbability = 0.015f;

    // Disaster names for displaying
    private string[] disasters = { "Earthquake", "Flood", "Wildfire" };
    private float[] disasterProbabilities;

    void Start()
    {
        // Initialize probabilities array
        disasterProbabilities = new float[] { earthquakeProbability, floodProbability, wildfireProbability };

        // Update UI with initial values
        UpdateResourceUI();

        // Start checking for disasters
        InvokeRepeating("CheckForDisasters", 10.0f, 10.0f); // Every 10 seconds
    }

    void Update()
    {
        ManageResources(); // Manage resources each frame
    }

    // Method to handle resource management
    void ManageResources()
    {
        // Example logic for reducing resources over time
        water -= 1;
        energy -= 2;
        food -= 1;

        // Update UI with new resource values
        UpdateResourceUI();

        // Check if resources are depleted
        if (water <= 0 || energy <= 0 || food <= 0)
        {
            GameOver();
        }
    }

    // Method to update the UI with current resources
    void UpdateResourceUI()
    {
        waterText.text = "Water: " + water;
        energyText.text = "Energy: " + energy;
        foodText.text = "Food: " + food;
        sustainabilityPointsText.text = "Sustainability Points: " + sustainabilityPoints;
    }

    // Method to check for random disasters
    void CheckForDisasters()
    {
        for (int i = 0; i < disasters.Length; i++)
        {
            if (Random.value < disasterProbabilities[i])
            {
                TriggerDisaster(disasters[i]);
                break;
            }
        }
    }

    // Method to trigger a disaster
    void TriggerDisaster(string disasterType)
    {
        disasterAlertText.text = "Disaster Occurred: " + disasterType;
        Debug.Log("Disaster Occurred: " + disasterType);

        // Reduce resources based on the disaster type
        switch (disasterType)
        {
            case "Earthquake":
                energy -= 200;
                break;
            case "Flood":
                water -= 300;
                break;
            case "Wildfire":
                food -= 150;
                break;
        }

        // Apply penalties and update UI
        sustainabilityPoints -= 50; // Penalty for disaster occurrence
        UpdateResourceUI();
    }

    // Game over logic
    void GameOver()
    {
        Debug.Log("Game Over: Resources depleted.");
        disasterAlertText.text = "Game Over! Resources Depleted.";
        CancelInvoke("CheckForDisasters"); // Stop checking for disasters
    }

    // Method to use renewable energy
    public void UseRenewableEnergy()
    {
        Debug.Log("Using Renewable Energy...");
        sustainabilityPoints += 100;
        energy += 500; // Increase energy resource
        UpdateResourceUI();
    }

    // Method to conserve water
    public void ConserveWater()
    {
        Debug.Log("Conserving Water...");
        sustainabilityPoints += 50;
        water += 300; // Increase water resource
        UpdateResourceUI();
    }

    // Method to start recycling
    public void StartRecycling()
    {
        Debug.Log("Starting Recycling Program...");
        sustainabilityPoints += 75;
        UpdateResourceUI();
    }

    // UI Button handlers
    public void OnRenewableEnergyButtonClicked()
    {
        UseRenewableEnergy();
    }

    public void OnConserveWaterButtonClicked()
    {
        ConserveWater();
    }

    public void OnStartRecyclingButtonClicked()
    {
        StartRecycling();
    }
}

