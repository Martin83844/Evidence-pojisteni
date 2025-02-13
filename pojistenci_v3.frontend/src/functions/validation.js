
export function setupValidation(formId) {
    document.addEventListener("DOMContentLoaded", function () {
        const form = document.getElementById(formId);

        form.addEventListener("submit", function (event) {
            if (!validateForm(formId)) {
                event.preventDefault();
            }
        });

        document.querySelectorAll(`#${formId} input`).forEach(input => {
            input.addEventListener("input", function () {
                document.getElementById(`${input.id}-error`).textContent = "";
            });
        });
    });
}

export function validateForm(formId) {
    let valid = true;
    const fields = {
        editInsurerAndInsuredForm: [
            { id: "name", message: "Jméno smí obsahovat pouze jedno slovo bez mezer." },
            { id: "surname", message: "Příjmení smí obsahovat pouze jedno slovo bez mezer." },
            { id: "email", message: "Zadejte platný e-mail ve formátu example@domain.com." },
            { id: "phoneNumber", message: "Telefonní číslo musí být ve formátu +420XXXXXXXXX." },
            { id: "city", message: "Název města smí obsahovat pouze písmena, čísla, mezery nebo spojovníky." },
            { id: "street", message: "Název ulice smí obsahovat písmena, čísla, mezery, pomlčky a lomítka. Čísla mohou obsahovat orientační a popisná čísla ve formátu '123', '123A' nebo '123/45'." },
            { id: "postcode", message: "PSČ musí mít 5 číslic nebo být ve formátu '123 45'." }
        ],
        editHomeInsuranceForm: [
            { id: "ownerName", message: "Jméno majitele musí obsahovat pouze dvě slova s mezerou." },
            { id: "ownerContact", message: "Telefonní číslo musí být ve formátu +420XXXXXXXXX." },
            { id: "price", message: "" },
            { id: "propertyValue", message: "" },
            { id: "validityUntil", message: "" }
        ],
        editCarInsuranceForm: [
            { id: "ownerName", message: "Jméno majitele musí obsahovat pouze dvě slova s mezerou." },
            { id: "ownerContact", message: "Telefonní číslo musí být ve formátu +420XXXXXXXXX." },
            { id: "price", message: "" },
            { id: "registrationNumber", message: "Zadejte platnou registrační značku (např. 1T02942, AB1234, 8B123)." },
            { id: "validityUntil", message: "" }
        ],
        addOrEditHomeDamageRecordForm: [
            { id: "date", message: "" },
            { id: "estimatedDamageCost", message: "" },
            { id: "approvedCompensation", message: "" },
            { id: "damagedPart", message: "" },
            { id: "description", message: "" }
        ],
        addOrEditCarAccidentRecordForm: [
            { id: "date", message: "" },
            { id: "estimatedDamageCost", message: "" },
            { id: "approvedCompensation", message: "" },
            { id: "damagedParts", message: "" },
            { id: "description", message: "" },
            { id: "otherPartiesInvolved", message: "" }
        ],
        addInsurerOrInsuredForm: [
            { id: "name", message: "Jméno smí obsahovat pouze jedno slovo bez mezer." },
            { id: "surname", message: "Příjmení smí obsahovat pouze jedno slovo bez mezer." },
            { id: "email", message: "Zadejte platný e-mail ve formátu example@domain.com." },
            { id: "phoneNumber", message: "Telefonní číslo musí být ve formátu +420XXXXXXXXX." },
            { id: "city", message: "Název města smí obsahovat pouze písmena, čísla, mezery nebo spojovníky." },
            { id: "street", message: "Název ulice smí obsahovat písmena, čísla, mezery, pomlčky a lomítka. Čísla mohou obsahovat orientační a popisná čísla ve formátu '123', '123A' nebo '123/45'." },
            { id: "postcode", message: "PSČ musí mít 5 číslic nebo být ve formátu '123 45'." },
            { id: "password", message: "Heslo musí obsahovat minimálně šest znaků, jedno velké písmeno, jeden speciální znak a jednu číslici bez mezer." },
            { id: "confirmPassword", message: "Hesla se neshodují." }
        ],
        addHomeInsuranceForm: [
            { id: "propertyAddress", message: "Zadejte adresu ve formátu: Město, Ulice Číslo (například 'Praha, Karlova 12')." },
            { id: "yearBuilt", message: "" },
            { id: "propertyValue", message: "" },
            { id: "propertyArea", message: "" },
            { id: "ownerName", message: "Jméno majitele musí obsahovat pouze dvě slova s mezerou." },
            { id: "ownerContact", message: "Telefonní číslo musí být ve formátu +420XXXXXXXXX." },
            { id: "price", message: "" },
            { id: "validityFrom", message: "" },
            { id: "validityUntil", message: "" },
            { id: "insurerId", message: "" },
            { id: "propertyType", message: "" },
            { id: "homeInsuranceType", message: "" }
        ],
        addCarInsuranceForm: [
            { id: "ownerName", message: "Jméno majitele musí obsahovat pouze dvě slova s mezerou." },
            { id: "ownerContact", message: "Telefonní číslo musí být ve formátu +420XXXXXXXXX." },
            { id: "registrationNumber", message: "" },
            { id: "price", message: "" },
            { id: "validityFrom", message: "" },
            { id: "validityUntil", message: "" },
            { id: "insurerId", message: "" },
            { id: "carInsuranceType", message: "" },
            { id: "fuelType", message: "" },
            { id: "usageType", message: "" },
            { id: "VIN", message: "" },
            { id: "make", message: "" },
            { id: "model", message: "" },
            { id: "yearOfManufacture", message: "" },
            { id: "engineSize", message: "" }
        ],
        anotherForm: [
            { id: "someField", message: "Toto pole je povinné." }
        ]
    };

    const selectedFields = fields[formId] || [];

    selectedFields.forEach(field => {
        const input = document.getElementById(field.id);
        const errorSpan = document.getElementById(`${field.id}-error`);

        if (input) {
            if (input.tagName === "SELECT" && input.value === "") {
                errorSpan.textContent = field.message || "Toto pole je povinné.";
                valid = false;
            } else if (!input.value.trim()) {
                errorSpan.textContent = "Toto pole je povinné.";
                valid = false;
            } else if (!input.checkValidity()) {
                errorSpan.textContent = field.message;
                valid = false;
            } else {
                errorSpan.textContent = "";
            }
        }
    });

    const passwordInput = document.getElementById("password");
    const confirmPasswordInput = document.getElementById("confirmPassword");
    const confirmPasswordError = document.getElementById("confirmPassword-error");

    if (passwordInput && confirmPasswordInput) {
        if (passwordInput.value !== confirmPasswordInput.value) {
            confirmPasswordError.textContent = "Hesla se neshodují.";
            valid = false;
        } else {
            confirmPasswordError.textContent = "";
        }
    }

    return valid;
}
