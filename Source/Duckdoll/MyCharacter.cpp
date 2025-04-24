/**
*	© 2025 Duckdoll. Created with Unreal Engine 5.5.3. Rights Reserved by Creator.
*/

#include "MyCharacter.h"
#include "EnhancedInputComponent.h"
#include "EnhancedInputSubsystems.h"
#include "InputMappingContext.h"
#include "InputAction.h"
#include "GameFramework/PlayerController.h"
#include "GameFramework/SpringArmComponent.h"
#include "GameFramework/CharacterMovementComponent.h"
#include "Camera/CameraComponent.h"
#include "Components/InputComponent.h"

AMyCharacter::AMyCharacter()
{
    // Set Skeletal Mesh
    static ConstructorHelpers::FObjectFinder<USkeletalMesh> SkeletalMeshAsset(TEXT("/Game/Characters/Mannequins/Meshes/SKM_Quinn"));
    if (SkeletalMeshAsset.Succeeded())
    {
        GetMesh()->SetSkeletalMesh(SkeletalMeshAsset.Object);

        // Adjust transform so the mesh isn't inside the floor
        GetMesh()->SetRelativeLocation(FVector(0.f, 0.f, -90.f));
        GetMesh()->SetRelativeRotation(FRotator(0.f, -90.f, 0.f));
    }

    // Set Animation Blueprint
    static ConstructorHelpers::FClassFinder<UAnimInstance> AnimBPClass(TEXT("/Game/Characters/Mannequins/Animations/ABP_Quinn"));
    if (AnimBPClass.Succeeded())
    {
        GetMesh()->SetAnimInstanceClass(AnimBPClass.Class);
    }

    static ConstructorHelpers::FObjectFinder<UInputMappingContext> IMC(TEXT("/Game/Input/IMC_Default"));
    if (IMC.Succeeded())
    {
        InputMappingContext = IMC.Object;
    }

    static ConstructorHelpers::FObjectFinder<UInputAction> MoveActionObj(TEXT("/Game/Input/Actions/IA_Move"));
    if (MoveActionObj.Succeeded())
    {
        MoveAction = MoveActionObj.Object;
    }

    static ConstructorHelpers::FObjectFinder<UInputAction> JumpActionObj(TEXT("/Game/Input/Actions/IA_Jump"));
    if (JumpActionObj.Succeeded())
    {
        JumpAction = JumpActionObj.Object;
    }

    static ConstructorHelpers::FObjectFinder<UInputAction> LookActionObj(TEXT("/Game/Input/Actions/IA_Look"));
    if (LookActionObj.Succeeded())
    {
        LookAction = LookActionObj.Object;
    }

    // Create and position the camera boom (spring arm)
    CameraBoom = CreateDefaultSubobject<USpringArmComponent>(TEXT("CameraBoom"));
    CameraBoom->SetupAttachment(RootComponent);
    CameraBoom->TargetArmLength = 300.0f; // Distance behind the character
    CameraBoom->bUsePawnControlRotation = true; // Rotate the arm based on controller

    // Create the camera and attach it to the boom
    FollowCamera = CreateDefaultSubobject<UCameraComponent>(TEXT("FollowCamera"));
    FollowCamera->SetupAttachment(CameraBoom, USpringArmComponent::SocketName);
    FollowCamera->bUsePawnControlRotation = false; // Camera does not rotate relative to arm

    // Character settings
    bUseControllerRotationYaw = false;
    GetCharacterMovement()->bOrientRotationToMovement = true; // Character moves in direction of input

}

void AMyCharacter::BeginPlay()
{
    Super::BeginPlay();

    if (APlayerController* PC = Cast<APlayerController>(Controller))
    {
        if (UEnhancedInputLocalPlayerSubsystem* Subsystem = ULocalPlayer::GetSubsystem<UEnhancedInputLocalPlayerSubsystem>(PC->GetLocalPlayer()))
        {
            if (InputMappingContext)
            {
                Subsystem->AddMappingContext(InputMappingContext, 0);
                UE_LOG(LogTemp, Warning, TEXT("Input Mapping Context added!"));
            }
            else
            {
                UE_LOG(LogTemp, Error, TEXT("InputMappingContext is NULL!"));
            }
        }
    }
}

void AMyCharacter::SetupPlayerInputComponent(UInputComponent* PlayerInputComponent)
{
    Super::SetupPlayerInputComponent(PlayerInputComponent);

    if (UEnhancedInputComponent* EnhancedInput = Cast<UEnhancedInputComponent>(PlayerInputComponent))
    {
        if (MoveAction)
        {
            EnhancedInput->BindAction(MoveAction, ETriggerEvent::Triggered, this, &AMyCharacter::Move);
        }

        if (JumpAction)
        {
            EnhancedInput->BindAction(JumpAction, ETriggerEvent::Started, this, &AMyCharacter::JumpPressed);
            EnhancedInput->BindAction(JumpAction, ETriggerEvent::Completed, this, &AMyCharacter::JumpReleased);
        }
        if (LookAction)
        {
            EnhancedInput->BindAction(LookAction, ETriggerEvent::Triggered, this, &AMyCharacter::Look);
        }
    }
}

void AMyCharacter::Move(const FInputActionValue& Value)
{
    FVector2D MovementVector = Value.Get<FVector2D>();
    if (Controller != nullptr)
    {
        const FRotator Rotation = Controller->GetControlRotation();
        const FRotator YawRotation(0, Rotation.Yaw, 0);

        const FVector ForwardDirection = FRotationMatrix(YawRotation).GetUnitAxis(EAxis::X);
        const FVector RightDirection = FRotationMatrix(YawRotation).GetUnitAxis(EAxis::Y);

        AddMovementInput(ForwardDirection, MovementVector.Y);
        AddMovementInput(RightDirection, MovementVector.X);
    }
}

void AMyCharacter::Look(const FInputActionValue& Value)
{
    FVector2D LookAxisVector = Value.Get<FVector2D>();
    AddControllerYawInput(LookAxisVector.X);
    AddControllerPitchInput(LookAxisVector.Y);

}

void AMyCharacter::JumpPressed()
{
    Jump();
}

void AMyCharacter::JumpReleased()
{
    StopJumping();
}

