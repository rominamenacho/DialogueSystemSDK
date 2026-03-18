DialogueSystemSDK
DialogueSystemSDK is a robust, flexible, and localized dialogue management solution designed for Unity. It allows developers to decouple dialogue data from UI logic, supporting dynamic language switching and structured JSON-based content management.

🚀 Key Features

Multi-Language Support: Centralized localization mapping where different languages (e.g., es, en) are stored as columns linked to a unique LineId.

Narrative Hierarchy: Organized by ChapterId and BlockId, allowing for complex storytelling and branching paths.

Audio Synchronization: Precise control over dialogue timing using AudioKey and Duration metadata.

Decoupled Logic: Separation of narrative flow, text translation, and audio assets for easier maintenance.

🛠 Tech Stack 

Engine: Unity 2022.x+

Language: C#

Data Format: JSON

Version Control: Git (Submodule compatible)

📦 Installation 
* Via Git URL
In Unity, open the Window > Package Manager.

Click the + button and select Add package from git URL...

Paste the URL: https://github.com/rominamenacho/DialogueSystemSDK.git

* Manual
Clone this repository into your project's Assets/Plugins folder:

* Bash
git clone https://github.com/rominamenacho/DialogueSystemSDK.git

📊 File Structure

The system is based on three main JSON file types that organize the narrative, text, and audio resources:

1. Narrative Structure (Structure.json)
   
Defines the game hierarchy by chapters, starting points, and dialogue blocks.

ChapterId: The unique identifier for the chapter (e.g., CH1, CH2).

StartBlockId: The specific block where a section or scene begins.

BlockId: The identifier for a group of dialogues (e.g., INTRO, PHONECALL).

LineId: The primary key that links the structure to the text and audio.

2. Localization (Localization.json)
   
Contains the translations mapped by LineId.

Supports multiple languages (e.g., es for Spanish and en for English columns).

3. Voice and Audio (Voice.json)
   
Associates each dialogue line with its corresponding audio file and length.

AudioKey: The name of the audio clip within the Unity Resources.

Duration: The playback time for the clip to ensure proper synchronization.


👤 Author 
Romina Menacho - Backend & Game Developer - GitHub

📄 License 
This project is licensed under the MIT License - see the LICENSE file for details.
