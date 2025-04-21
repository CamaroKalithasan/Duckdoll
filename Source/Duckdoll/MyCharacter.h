/**
*	© 2025 Duckdoll. Created with Unreal Engine 5.5.3. Rights Reserved by Creator.
*/

#pragma once

#include "CoreMinimal.h"
#include "GameFramework/Character.h"
#include "InputActionValue.h"
#include "MyCharacter.generated.h"

class UInputAction;

/**
 * Character Base.
 */
UCLASS()
class DUCKDOLL_API AMyCharacter : public ACharacter
{
	GENERATED_BODY()

public:
    AMyCharacter();

protected:
    virtual void BeginPlay() override;
    virtual void SetupPlayerInputComponent(class UInputComponent* PlayerInputComponent) override;

protected:

    UPROPERTY(VisibleAnywhere, BlueprintReadOnly, Category = "Components")
    class USkeletalMeshComponent* CharacterMesh;

    UPROPERTY(VisibleAnywhere, BlueprintReadOnly, Category = Camera)
    class USpringArmComponent* CameraBoom;

    UPROPERTY(VisibleAnywhere, BlueprintReadOnly, Category = Camera)
    class UCameraComponent* FollowCamera;

    UPROPERTY(EditDefaultsOnly, Category = Input)
    UInputAction* LookAction;


private:
    void Move(const FInputActionValue& Value);
    void Look(const FInputActionValue& Value);
    void JumpPressed();
    void JumpReleased();

    UPROPERTY(EditDefaultsOnly, Category = "Input")
    class UInputMappingContext* InputMappingContext;

    UPROPERTY(EditDefaultsOnly, Category = "Input")
    class UInputAction* MoveAction;

    UPROPERTY(EditDefaultsOnly, Category = "Input")
    class UInputAction* JumpAction;
};
