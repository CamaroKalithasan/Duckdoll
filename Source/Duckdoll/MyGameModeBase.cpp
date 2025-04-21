/**
*	© 2025 Duckdoll. Created with Unreal Engine 5.5.3. Rights Reserved by Creator.
*/

#include "MyGameModeBase.h"
#include "MyCharacter.h"

AMyGameModeBase::AMyGameModeBase()
{
	DefaultPawnClass = AMyCharacter::StaticClass();
}
