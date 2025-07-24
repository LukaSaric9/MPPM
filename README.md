# MPPM
A uni project for subject Data models in smart grid systems.

# CIM-Based Network Model System

This project is a .NET-based system for modeling and managing electrical networks using **CIM (Common Information Model)** standards. It supports importing UML/XMI-based models, generates RDFS and RDF definitions, and creates a working concrete model used by a **GDA (Generic Data Access)** service and a **WPF GUI** frontend built using the **MVVM** pattern.

## 🧩 Technologies Used

- ✅ .NET (C#)
- ✅ WPF (GUI with MVVM)
- ✅ UML/XMI modeling (via EA tool)
- ✅ RDF, RDFS, XMI
- ✅ DLL generation
- ✅ CIM IEC standards (TC57)

- **UML/XMI Model**  
  - Based on IEC CIM standards  
  - Created in Enterprise Architect (EA)
- **Model Transformation Tools**  
  - Generates RDFS and a DLL from the UML
- **GDA (Generic Data Access)**  
  - Acts as a host exposing the CIM data model
- **WPF GUI (Client App)**  
  - Implements core GDA operations:  
    `GetValues`, `GetExtentValues`, `GetRelatedValues`
  - Clean separation via MVVM

  ## 🚀 How to Run

1. **Start the GDA Host (NetworkModelService)**
   - Run the `.exe` or start the service from Visual Studio.

2. **Start the GUI (WPF App)**
   - Run the WPF app that connects to GDA.
   - Use the interface to invoke methods like `GetValues`, `GetExtentValues`, etc.

   
