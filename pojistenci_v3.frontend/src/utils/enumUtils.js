export const HomeInsuranceType = {
    Fire: "Pojištění proti požáru",
    Theft: "Pojištění proti krádeži",
    Flood: "Pojištění proti povodním",
    AllRisk: "Pojištění všech rizik"
};

export const PropertyType = {
    House: "Rodinný dům",
    Apartment: "Byt",
    Commercial: "Komerční budova",
    Cottage: "Chata / Chalupa",
    Other: "Jiný"
};

export const CarInsuranceType = {
    Liability: "Povinné ručení",
    Collision: "Havarijní pojištění",
    Comprehensive: "Komplexní pojištění",
    Theft: "Pojištění proti krádeži"
};

export const FuelType = {
    Petrol: "Benzín",
    Diesel: "Nafta",
    Electric: "Elektromobil",
    Hybrid: "Hybrid",
    CNG: "CNG",
    LPG: "LPG"
};

export const UsageType = {
    Personal: "Osobní",
    Commercial: "Komerční"
};

export const getEnumDisplayName = (enumObj, enumValue) => {
    return enumObj[enumValue] || 'Neznámý typ pojištění';
};