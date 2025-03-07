using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SkinCancerDetectionAPI.Migrations.ModelPrediction
{
    /// <inheritdoc />
    public partial class NinethMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admin",
                columns: table => new
                {
                    AdminId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    DateOfBirth = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    PhysicalAddress = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    EmailAddress = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    DateCreated = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    DateUpdated = table.Column<string>(type: "nvarchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admin", x => x.AdminId);
                });

            migrationBuilder.CreateTable(
                name: "AdminAuthentication",
                columns: table => new
                {
                    AdminAuthenticationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdminId = table.Column<int>(type: "int", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    RecoveryQuestion = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    RecoveryAnswer = table.Column<string>(type: "nvarchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminAuthentication", x => x.AdminAuthenticationId);
                    table.ForeignKey(
                        name: "FK_AdminAuthentication_Admin_AdminId",
                        column: x => x.AdminId,
                        principalTable: "Admin",
                        principalColumn: "AdminId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Dermatologist",
                columns: table => new
                {
                    DermatologistId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdminId = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    DateOfBirth = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    PhysicalAddress = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    EmailAddress = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    DateCreated = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    DateUpdated = table.Column<string>(type: "nvarchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dermatologist", x => x.DermatologistId);
                    table.ForeignKey(
                        name: "FK_Dermatologist_Admin_AdminId",
                        column: x => x.AdminId,
                        principalTable: "Admin",
                        principalColumn: "AdminId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Authentication",
                columns: table => new
                {
                    AuthenticationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DermatologistId = table.Column<int>(type: "int", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    RecoveryQuestion = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    RecoveryAnswer = table.Column<string>(type: "nvarchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authentication", x => x.AuthenticationId);
                    table.ForeignKey(
                        name: "FK_Authentication_Dermatologist_DermatologistId",
                        column: x => x.DermatologistId,
                        principalTable: "Dermatologist",
                        principalColumn: "DermatologistId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Patient",
                columns: table => new
                {
                    PatientId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DermatologistId = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    DateOfBirth = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    PhysicalAddress = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    EmailAddress = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    MedicalHistory = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    FamilyHistory = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    SkinType = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    DateCreated = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    DateUpdated = table.Column<string>(type: "nvarchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patient", x => x.PatientId);
                    table.ForeignKey(
                        name: "FK_Patient_Dermatologist_DermatologistId",
                        column: x => x.DermatologistId,
                        principalTable: "Dermatologist",
                        principalColumn: "DermatologistId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Lesion",
                columns: table => new
                {
                    LesionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    LesionLocation = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    DateOfDetection = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    DateOfExamination = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    LesionDescription = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    DermoscopicFeatures = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    IsBiopsy = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    BiopsyDate = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    BiopsyResult = table.Column<string>(type: "nvarchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lesion", x => x.LesionId);
                    table.ForeignKey(
                        name: "FK_Lesion_Patient_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patient",
                        principalColumn: "PatientId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Diagnosis",
                columns: table => new
                {
                    DiagnosisId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LesionId = table.Column<int>(type: "int", nullable: false),
                    DiagnosisDate = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    DiagnosisResult = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    DiagnosisDescription = table.Column<string>(type: "nvarchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diagnosis", x => x.DiagnosisId);
                    table.ForeignKey(
                        name: "FK_Diagnosis_Lesion_LesionId",
                        column: x => x.LesionId,
                        principalTable: "Lesion",
                        principalColumn: "LesionId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Image",
                columns: table => new
                {
                    ImageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LesionId = table.Column<int>(type: "int", nullable: false),
                    ImageType = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    ImageDescription = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Resolution = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    DateTaken = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    CameraModel = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    IlluminationType = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    DateUpdated = table.Column<string>(type: "nvarchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Image", x => x.ImageId);
                    table.ForeignKey(
                        name: "FK_Image_Lesion_LesionId",
                        column: x => x.LesionId,
                        principalTable: "Lesion",
                        principalColumn: "LesionId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "ModelPredictions",
                columns: table => new
                {
                    ModelPredictionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageId = table.Column<int>(type: "int", nullable: false),
                    PredictionResult = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    ConfidenceScore = table.Column<string>(type: "nvarchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModelPredictions", x => x.ModelPredictionId);
                    table.ForeignKey(
                        name: "FK_ModelPredictions_Image_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Image",
                        principalColumn: "ImageId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Treatment",
                columns: table => new
                {
                    TreatmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DiagnosisId = table.Column<int>(type: "int", nullable: false),
                    ModelPredictionId = table.Column<int>(type: "int", nullable: false),
                    TreatmentDate = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    TreatmentType = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    TreatmentDescription = table.Column<string>(type: "nvarchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Treatment", x => x.TreatmentId);
                    table.ForeignKey(
                        name: "FK_Treatment_Diagnosis_DiagnosisId",
                        column: x => x.DiagnosisId,
                        principalTable: "Diagnosis",
                        principalColumn: "DiagnosisId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Treatment_ModelPredictions_ModelPredictionId",
                        column: x => x.ModelPredictionId,
                        principalTable: "ModelPredictions",
                        principalColumn: "ModelPredictionId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "FollowUp",
                columns: table => new
                {
                    FollowUpId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TreatmentId = table.Column<int>(type: "int", nullable: false),
                    FollowUpDate = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    FollowUpResult = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    FollowUpDescription = table.Column<string>(type: "nvarchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FollowUp", x => x.FollowUpId);
                    table.ForeignKey(
                        name: "FK_FollowUp_Treatment_TreatmentId",
                        column: x => x.TreatmentId,
                        principalTable: "Treatment",
                        principalColumn: "TreatmentId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "AuditLog",
                columns: table => new
                {
                    AuditLogId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdminId = table.Column<int>(type: "int", nullable: false),
                    AdminAuthenticationId = table.Column<int>(type: "int", nullable: false),
                    DermatologistId = table.Column<int>(type: "int", nullable: false),
                    AuthenticationId = table.Column<int>(type: "int", nullable: false),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    LesionId = table.Column<int>(type: "int", nullable: false),
                    DiagnosisId = table.Column<int>(type: "int", nullable: false),
                    ImageId = table.Column<int>(type: "int", nullable: false),
                    ModelPredictionId = table.Column<int>(type: "int", nullable: false),
                    TreatmentId = table.Column<int>(type: "int", nullable: false),
                    FollowUpId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLog", x => x.AuditLogId);
                    table.ForeignKey(
                        name: "FK_AuditLog_AdminAuthentication_AdminAuthenticationId",
                        column: x => x.AdminAuthenticationId,
                        principalTable: "AdminAuthentication",
                        principalColumn: "AdminAuthenticationId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_AuditLog_Admin_AdminId",
                        column: x => x.AdminId,
                        principalTable: "Admin",
                        principalColumn: "AdminId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_AuditLog_Authentication_AuthenticationId",
                        column: x => x.AuthenticationId,
                        principalTable: "Authentication",
                        principalColumn: "AuthenticationId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_AuditLog_Dermatologist_DermatologistId",
                        column: x => x.DermatologistId,
                        principalTable: "Dermatologist",
                        principalColumn: "DermatologistId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_AuditLog_Diagnosis_DiagnosisId",
                        column: x => x.DiagnosisId,
                        principalTable: "Diagnosis",
                        principalColumn: "DiagnosisId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_AuditLog_FollowUp_FollowUpId",
                        column: x => x.FollowUpId,
                        principalTable: "FollowUp",
                        principalColumn: "FollowUpId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_AuditLog_Image_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Image",
                        principalColumn: "ImageId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_AuditLog_Lesion_LesionId",
                        column: x => x.LesionId,
                        principalTable: "Lesion",
                        principalColumn: "LesionId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_AuditLog_ModelPredictions_ModelPredictionId",
                        column: x => x.ModelPredictionId,
                        principalTable: "ModelPredictions",
                        principalColumn: "ModelPredictionId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_AuditLog_Patient_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patient",
                        principalColumn: "PatientId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_AuditLog_Treatment_TreatmentId",
                        column: x => x.TreatmentId,
                        principalTable: "Treatment",
                        principalColumn: "TreatmentId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdminAuthentication_AdminId",
                table: "AdminAuthentication",
                column: "AdminId");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLog_AdminAuthenticationId",
                table: "AuditLog",
                column: "AdminAuthenticationId");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLog_AdminId",
                table: "AuditLog",
                column: "AdminId");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLog_AuthenticationId",
                table: "AuditLog",
                column: "AuthenticationId");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLog_DermatologistId",
                table: "AuditLog",
                column: "DermatologistId");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLog_DiagnosisId",
                table: "AuditLog",
                column: "DiagnosisId");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLog_FollowUpId",
                table: "AuditLog",
                column: "FollowUpId");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLog_ImageId",
                table: "AuditLog",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLog_LesionId",
                table: "AuditLog",
                column: "LesionId");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLog_ModelPredictionId",
                table: "AuditLog",
                column: "ModelPredictionId");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLog_PatientId",
                table: "AuditLog",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLog_TreatmentId",
                table: "AuditLog",
                column: "TreatmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Authentication_DermatologistId",
                table: "Authentication",
                column: "DermatologistId");

            migrationBuilder.CreateIndex(
                name: "IX_Dermatologist_AdminId",
                table: "Dermatologist",
                column: "AdminId");

            migrationBuilder.CreateIndex(
                name: "IX_Diagnosis_LesionId",
                table: "Diagnosis",
                column: "LesionId");

            migrationBuilder.CreateIndex(
                name: "IX_FollowUp_TreatmentId",
                table: "FollowUp",
                column: "TreatmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Image_LesionId",
                table: "Image",
                column: "LesionId");

            migrationBuilder.CreateIndex(
                name: "IX_Lesion_PatientId",
                table: "Lesion",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_ModelPredictions_ImageId",
                table: "ModelPredictions",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Patient_DermatologistId",
                table: "Patient",
                column: "DermatologistId");

            migrationBuilder.CreateIndex(
                name: "IX_Treatment_DiagnosisId",
                table: "Treatment",
                column: "DiagnosisId");

            migrationBuilder.CreateIndex(
                name: "IX_Treatment_ModelPredictionId",
                table: "Treatment",
                column: "ModelPredictionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuditLog");

            migrationBuilder.DropTable(
                name: "AdminAuthentication");

            migrationBuilder.DropTable(
                name: "Authentication");

            migrationBuilder.DropTable(
                name: "FollowUp");

            migrationBuilder.DropTable(
                name: "Treatment");

            migrationBuilder.DropTable(
                name: "Diagnosis");

            migrationBuilder.DropTable(
                name: "ModelPredictions");

            migrationBuilder.DropTable(
                name: "Image");

            migrationBuilder.DropTable(
                name: "Lesion");

            migrationBuilder.DropTable(
                name: "Patient");

            migrationBuilder.DropTable(
                name: "Dermatologist");

            migrationBuilder.DropTable(
                name: "Admin");
        }
    }
}
