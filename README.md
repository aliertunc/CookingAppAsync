# CookingApp Async

This code is for a Windows Forms application in C# that allows a user to cook eggs using different cooking methods. It has two main buttons: one to start cooking eggs synchronously, and another to start cooking eggs asynchronously.

The frmMain class contains two methods for cooking eggs: btnStartCookingSync_Click and btnAsync_Click. The first method starts the cooking process synchronously, meaning that each step in the process is executed one after the other, blocking the UI thread until the process is complete. The second method starts the cooking process asynchronously, using the async and await keywords to allow for concurrent execution of tasks and non-blocking of the UI thread.

The cooking process consists of several steps, each of which is represented by a separate method. These methods simulate the time it takes to complete each step by using the Task.Delay method to pause execution for a specified number of milliseconds. The methods also update the log displayed in the UI to show the progress of the cooking process.

The AdjustButtons method is used to disable and enable buttons in the UI as the cooking process progresses. The AddLog method is used to add log messages to the UI.
