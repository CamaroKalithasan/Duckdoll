// Copyright Epic Games, Inc. All Rights Reserved.

#include "DuckdollGameMode.h"
#include "DuckdollCharacter.h"
#include "UObject/ConstructorHelpers.h"

ADuckdollGameMode::ADuckdollGameMode()
{
	// set default pawn class to our Blueprinted character
	static ConstructorHelpers::FClassFinder<APawn> PlayerPawnBPClass(TEXT("/Game/ThirdPerson/Blueprints/BP_ThirdPersonCharacter"));
	if (PlayerPawnBPClass.Class != NULL)
	{
		DefaultPawnClass = PlayerPawnBPClass.Class;
	}
}
