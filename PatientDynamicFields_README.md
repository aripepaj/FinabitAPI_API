# Patient Dynamic Fields System

This system allows configurable patient registration fields per department, similar to the UI shown in the image.

## Features

- **Dynamic Field Configuration**: Configure fields per department with:
  - Field labels (display names)
  - Field types (text, number, date, dropdown, checkbox, textarea)
  - Required/Optional settings
  - Enable/Disable functionality
  - Display order
  - Validation rules
  - Help text

- **Field Types Supported**:
  - `text` - Single line text input
  - `textarea` - Multi-line text input
  - `number`/`decimal` - Numeric input
  - `date`/`datetime` - Date picker
  - `dropdown` - Select from predefined options
  - `checkbox`/`boolean` - True/False toggle

## API Endpoints

### Field Configuration Management

#### Get Field Configuration
```
GET /api/clinic/patient-fields?departmentId={id}
```
Returns field definitions for a specific department (or global if no department specified).

#### Initialize Default Fields
```
POST /api/clinic/patient-fields/initialize/{departmentId}
```
Creates default field definitions based on your image:
- Nr. personal (text)
- Alergjitë (textarea)
- Grupi i gjakut (dropdown: A+, A-, B+, B-, AB+, AB-, O+, O-)
- Tensioni i gjakut (text)
- Pulsacionet (number)
- Historiku i sëmundjeve kardiake në familje (textarea)
- Medikamentet e përdorura (textarea)

#### Save/Update Field Definition
```
POST /api/clinic/patient-fields
PUT /api/clinic/patient-fields/{id}
```

#### Delete Field Definition
```
DELETE /api/clinic/patient-fields/{id}
```

### Patient Data with Dynamic Fields

#### Get Patient with Dynamic Fields
```
GET /api/clinic/patients/{id}/with-fields?departmentId={id}
```
Returns patient data combined with dynamic field values and field definitions.

#### Save Patient Dynamic Fields
```
POST /api/clinic/patients/{id}/dynamic-fields
{
  "fieldValues": {
    "nr_personal": "1234567890",
    "alergjite": "No known allergies",
    "grupi_gjakut": "A+",
    "tensioni_gjakut": "120/80",
    "pulsacionet": 72,
    "historiku_semundjeve": "No family history of heart disease",
    "medikamentet": "Aspirin 81mg daily"
  }
}
```

#### Get All Patients with Dynamic Fields
```
GET /api/clinic/patients/with-fields?departmentId={id}
```

## Usage Example

1. **Initialize default fields for a department**:
```bash
POST /api/clinic/patient-fields/initialize/1
```

2. **Configure a field** (enable/disable, make required, etc.):
```json
PUT /api/clinic/patient-fields/3
{
  "departmentID": 1,
  "fieldKey": "grupi_gjakut",
  "displayName": "Grupi i gjakut",
  "fieldType": "dropdown",
  "fieldOptions": "[\"A+\",\"A-\",\"B+\",\"B-\",\"AB+\",\"AB-\",\"O+\",\"O-\"]",
  "isEnabled": true,
  "isRequired": true,
  "displayOrder": 3,
  "width": 6
}
```

3. **Save patient dynamic field data**:
```json
POST /api/clinic/patients/123/dynamic-fields
{
  "fieldValues": {
    "nr_personal": "1234567890",
    "grupi_gjakut": "A+",
    "pulsacionet": 75
  }
}
```

4. **Retrieve patient with all field data**:
```bash
GET /api/clinic/patients/123/with-fields?departmentId=1
```

## Database Tables

- `tblCLPatientFieldDefinitions` - Field configuration per department
- `tblCLPatientFieldValues` - Actual field values per patient
- `tblCLPatients` - Core patient data

## Field Configuration Properties

Each field definition supports:
- `FieldKey` - Unique identifier (e.g., "nr_personal")
- `DisplayName` - UI label (e.g., "Nr. personal")
- `FieldType` - Input type (text, number, date, etc.)
- `FieldOptions` - JSON array for dropdown/checkbox options
- `IsEnabled` - Show/hide field (checkbox in UI)
- `IsRequired` - Mark as required (exclamation mark in UI)
- `DisplayOrder` - Sort order in form
- `ValidationRules` - Custom validation (JSON)
- `HelpText` - Tooltip or placeholder text
- `Width` - Bootstrap grid width (1-12)
- `IsVisible` - Visibility control
- `IsReadOnly` - Read-only fields

This system provides the same flexibility shown in your image with programmatic control over field configuration.