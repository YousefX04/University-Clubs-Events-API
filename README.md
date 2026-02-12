# 🎓 University Clubs & Events Management System API

A robust ASP.NET Core Web API designed to manage university clubs and events using role-based access control and a structured approval workflow.

---

## 📌 Overview

The University Clubs & Events Management System API provides a backend solution for managing:

- University clubs
- Events organization
- Student memberships
- Event registrations
- Approval workflows
- Role-based dashboards

The system ensures that all major operations go through a controlled approval process to maintain integrity and administrative oversight.

---

## 👥 System Roles

### 👨‍💼 Admin
- Approve or reject club creation requests
- Approve or reject event creation requests
- Approve or reject updates to clubs and events
- Access system dashboard

### 👨‍🏫 Club Leader
- Create clubs (Pending Admin approval)
- Create events (Pending Admin approval)
- Update clubs and events (Pending Admin approval)
- Accept or reject student join requests
- Remove students from clubs or events
- Access club leader dashboard

### 👨‍🎓 Student
- View approved clubs and events
- Request to join clubs (Pending Club Leader approval)
- Request to join events (Pending Club Leader approval)
- Access student dashboard

---

## 🔄 Approval Workflow

The system implements a structured approval process:

- Club Creation → Pending → Admin Approval
- Event Creation → Pending → Admin Approval
- Club/Event Update → Pending → Admin Approval
- Student Join Request → Pending → Club Leader Approval

This ensures secure and controlled data management.

---

## 🏗️ Architecture

The project follows a **3-Tier Architecture**:

- **Presentation Layer (PL)** – ASP.NET Core Web API (Controllers)
- **Business Logic Layer (BLL)** – Service Layer
- **Data Access Layer (DAL)** – Repository Pattern + Unit of Work

---

## 🧱 Design Patterns Used

- Repository Pattern
- Unit of Work Pattern
- Service Layer Pattern
- Dependency Injection
- Separation of Concerns
- Code-First Approach

---

## 🔐 Authentication & Authorization

- ASP.NET Core Identity
- JWT (JSON Web Token) Authentication
- Role-Based Authorization (Admin / ClubLeader / Student)

Features:
- Secure login & registration
- Protected endpoints by role
- Token-based authentication

---

## ✅ Validation

- FluentValidation for clean and maintainable input validation
- Centralized validation logic separated from controllers

---

## 🗄️ Database Design

The system uses SQL Server with Entity Framework Core (Code-First approach).  
It is designed around a structured approval workflow and relationship-based data modeling.

### 👤 Identity & User Management

- **AppUser** → Inherits from ASP.NET Core IdentityUser for authentication.
- **User** → Application-specific user profile linked to Identity via `AppUserId`.

This separation allows:
- Secure authentication using Identity
- Clean domain modeling for business logic

---

## 🏫 Core Entities

### Club
Represents a university club.

- Id
- ClubName
- Description
- CreatedAt
- ClubLeaderID (FK → User)
- Status (Pending / Approved / Rejected)

Relationships:
- One Club → Many Events
- One Club → Many Members (ClubMember)

---

### Event
Represents an event created under a club.

- Id
- EventName
- Description
- CreatedAt
- StartAt
- EndAt
- Status (Pending / Approved / Rejected)
- ClubID (FK → Club)

Relationships:
- One Event → Many Event Members

---

## 👥 Membership Entities (Many-to-Many with Status)

### ClubMember
Represents a student joining a club.

- ClubID (FK)
- UserID (FK)
- Status (Pending / Approved / Rejected)

This allows controlled membership approval by the Club Leader.

---

### EventMember
Represents a student joining an event.

- EventID (FK)
- UserID (FK)
- Status (Pending / Approved / Rejected)

This enables event participation approval.

---

## 🔄 Update Tracking Entities

To maintain approval control over updates, the system uses dedicated update tables:

### ClubUpdate
Stores pending modifications before admin approval.

- OldName / NewName
- OldDescription / NewDescription

---

### EventUpdate
Stores pending event modifications before admin approval.

- OldName / NewName
- OldStart / NewStart
- OldEnd / NewEnd
- OldDescription / NewDescription

This design ensures:
- No direct modification of approved data
- Administrative control over changes
- Clear audit trail of modifications

---

## 🔄 Status-Based Workflow

Multiple entities use a **Status field** to control their lifecycle:

- Pending
- Approved
- Rejected

This applies to:
- Clubs
- Events
- ClubMember
- EventMember

The status-driven workflow enforces business rules and prevents unauthorized data exposure.

---

## 🛠️ Technologies Used

- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- ASP.NET Core Identity
- JWT Authentication
- FluentValidation
- Repository Pattern
- Unit of Work
- 3-Tier Architecture

---

## 🚀 Getting Started

### 1️⃣ Clone the Repository

```bash
git clone https://github.com/your-username/University-Clubs-Events-API.git
```

### 2️⃣ Configure Database

Update your `appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER;Database=ClubsEventsDb;Trusted_Connection=True;"
}
```

### 3️⃣ Apply Migrations

```bash
Update-Database
```

### 4️⃣ Run the Application

```bash
dotnet run
```

---

## 📈 Future Improvements

- Pagination & Filtering
- Email notifications for approvals
- Logging & Global Exception Handling
- Caching for dashboards
- Unit & Integration Testing

---

## 👨‍💻 Author

**Yousef Ahmed Fawzy**

- Backend Developer
- ASP.NET Core Enthusiast
