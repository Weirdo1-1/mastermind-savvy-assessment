# Mastermind Console Game in C#

A Windows Console version of the classic Mastermind game using C# and solid object-oriented programming (OOP) principles.

## Features

- Secret code consists of 4 distinct digits from `0` to `8`
- Player has 10 attempts by default (customizable via startup parameter)
- Supports startup parameters:
  - `-c [CODE]`: Manually set the secret code
  - `-t [ATTEMPTS]`: Set the number of attempts
- Console input validation with feedback:
  - Well placed pieces (correct digit, correct position)
  - Misplaced pieces (correct digit, wrong position)
- Graceful handling of invalid input and EOF (Ctrl+D)

## How to Run

### Using .NET CLI (Recommended)

Make sure you have the .NET SDK installed.

```bash
# Navigate to the project folder
cd MastermindGame

# Run with default settings
dotnet run

# Run with custom secret code
dotnet run -- -c "0123"

# Run with custom attempts
dotnet run -- -t 15

# Both options
dotnet run -- -c "0123" -t 12
```

### Using Compiled Executable

```bash
# Build the project first
dotnet build

# Run the executable
./bin/Debug/net6.0/MastermindGame.exe -c "0123"
```

## Gameplay Instructions

The game prompts: "Will you find the secret code? Please enter a valid guess"

- Enter 4 unique digits (0–8) per guess
- After each guess, feedback is provided:
  - Well placed pieces: X
  - Misplaced pieces: Y
- Winning message: "Congratz! You did it!"
- On invalid input (wrong length, repeating digits, invalid characters): "Wrong input!"
- Handles Ctrl+D (EOF) to exit early

## Example Game

```
Will you find the secret code?
Please enter a valid guess
--- Round 0
>1456
Well placed pieces: 0
Misplaced pieces: 1
--- Round 1
>tata
Wrong input!
--- Round 1
>4132
Well placed pieces: 1
Misplaced pieces: 2
--- Round 2
>0123
Congratz! You did it!
```

## Code Structure

The game uses object-oriented programming with separate classes for:
- **MastermindGame**: Main game logic and flow
- **CodeGenerator**: Random code generation
- **CodeValidator**: Input validation
- **FeedbackCalculator**: Calculating well-placed and misplaced pieces

Created for Savvy Kickstarter Program 2025 - Gameplay Programming Track