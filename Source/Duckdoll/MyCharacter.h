/**
*	© 2025 Duckdoll. Created with Unreal Engine 5.5.3. Rights Reserved by Creator.
*/

#pragma once

#include "CoreMinimal.h"
#include "GameFramework/Character.h"
#include "InputActionValue.h"
#include "MyCharacter.generated.h"

class UInputAction;
class UInputMappingContext;

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

    // Input handlers
    void LightAttack();
    void HeavyAttack();

    // Knockback function
    void TakeDamageWithKnockback(float DamageAmount, FVector HitDirection);

protected:

    UPROPERTY(VisibleAnywhere, BlueprintReadOnly, Category = "Components")
    class USkeletalMeshComponent* CharacterMesh;

    UPROPERTY(VisibleAnywhere, BlueprintReadOnly, Category = Camera)
    class USpringArmComponent* CameraBoom;

    UPROPERTY(VisibleAnywhere, BlueprintReadOnly, Category = Camera)
    class UCameraComponent* FollowCamera;

    UPROPERTY(EditDefaultsOnly, Category = Input)
    UInputAction* LookAction;

    UPROPERTY(EditDefaultsOnly, Category = Input)
    UInputAction* LightAttackAction;

    UPROPERTY(EditDefaultsOnly, Category = Input)
    UInputAction* HeavyAttackAction;

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

    UPROPERTY(EditAnywhere, Category = "Combat")
    float Durability = 0.0f;

    UPROPERTY(EditAnywhere, Category = "Combat")
    float MaxDurability = 999.0f;

    UPROPERTY(EditAnywhere, Category = "Combat")
    float BaseKnockback = 200.0f;
};
