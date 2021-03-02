# WPF MVVM Demo



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



## To do

- [x] ViewModelLocator (Feature)
- [x] NavigationService (Feature)
- [x] RelayCommands (Feature)
- [x] AsyncRelayCommandsConverters (Feature)
- [x] InDesingMode (Freature)
- [x] Global Exception Handler (Feature)
- [x] DemoService (Demo)
- [x] Messager (Event Hub) (Demo)
- [x] Controls (Demo)
- [x] Models + Validation (Demo)
- [x] Model Collection (Demo)
- [x] Viewmodels (Demo)
- [x] Localization (Demo)
- [x] IDataErrorInfo (Models)
- [x] App Settings (Demo)
- [x] Logging (Feature)
- [ ] Entity Framework (Sqlite) (Demo)
- [ ] MS Graph Service (Feature)
- [ ] Ansible Service (Feature)
- [ ] Telerik (Demo)
- [ ] MahApps.Metro  (Demo)
- [ ] VS Template
- [ ] User Secrets (Demo)
