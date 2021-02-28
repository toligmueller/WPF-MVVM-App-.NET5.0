# Model View ViewModel



## Structure

### Visual Layer

#### Views

- UI/UX
- Code behind only for UI/UX manipulation
- Windows & pages

#### Converters

- Methods to convert values in view

#### Controls

- User controls
- Code behind allowed 
- Dependency properties

#### Templates

- ???

### Logic Layer

#### Viewmodels

- UI/UX app logic 
- Communication between viewmodels by messenger (event bus)
- INotifyPropertyChanged (Observable-Object/Recipient)

##### Services

- Business logic
- Data manipulation
- DB integration
- Navigation
  - AddSingleton: Global
  - AddTransient: Temporary
  - AddScoped: ???

### Data Layer

#### Models

- Data runtime storage
- Validation (IDataErrorInfo)
- Communication between models by messenger (event bus)
- INotifyPropertyChanged (Observable-Object/Recipient)

### Base Layer

#### Extensions

- Package manipulation & extension

#### Messages

- Collection of messages for the event bus



## Logging

#### Levels:

- Verbose
- Debug
- Information
- Warning
- Error
- Fatal



## To do (Base)

- [x] ViewModelLocator
- [x] NavigationService
- [x] RelayCommands
- [x] AsyncRelayCommandsConverters
- [x] InDesingMode
- [x] Global Exception Handler
- [x] Global Instance Navigation
- [x] DemoService
- [x] Messenger (Send/Receive)
- [ ] Messenger (Request)
- [x] Messages
- [x] Controls
- [ ] Templates
- [x] Models
- [x] Model Collection
- [x] Viewmodels
- [x] Localization
- [x] IDataErrorInfo (Models)
- [x] App Settings
- [x] Logging
- [x] Logging with App Settings
- [ ] Documentation (in code)
- [ ] Documentation (readme)



## To do (Extended)

- [ ] DB Service (MSQL)
- [ ] DB Service (PostgreSQL)
- [ ] DB Service (MariaDB)
- [ ] MS Graph Service
- [ ] Ansible Service
- [ ] Telerik 
- [ ] MahApps.Metro
- [ ] Messages with Channel Token
- [ ] Independent Instance Navigation 