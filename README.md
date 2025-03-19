# Windows Session Manager

*Türkçe versiyon için, lütfen [README.tr.md](README.tr.md)'e bakın.*


Windows Session Manager is a Windows Service application developed in C#. This application automatically closes user sessions that have been disconnected and have exceeded a specified duration (default is 60 minutes) using a background timer.

## Features

- **Automatic Session Closure:** Closes user sessions that have been disconnected and have exceeded 60 minutes.
- **Whitelist Support:** Provides a whitelist feature to prevent specific users from being closed.
- **Easy Configuration:** Configure session timeout and whitelist settings easily via the `app.config` file.

## Requirements

- .NET Framework (4.7.2)

## Installation

1. **Download or Clone the Project:**

   ```bash
   git clone https://github.com/kemalincekara/WindowsSessionManager.git
   ```

2. **Open the Project:**
   
   Open the project using Visual Studio 2022.

3. **Build the Project:**

   In Visual Studio 2022, go to **Build > Build Solution** (or press Ctrl+Shift+B) to compile the project.

4. **Service Installation Process:**

   The service operations are performed using command line arguments:
   
   - **Install:** `-i` or `--install`
   - **Uninstall:** `-u` or `--uninstall`
   - **Start:** `-s` or `--start`
   - **Stop:** `-t` or `--stop`

   Example usage:

   ```bash
   WindowsSessionManager.exe --install
   ```

## Configuration

You can adjust the settings in the `app.config` file:

- **TimerIntervalMinutes:** The interval at which the timer runs (in minutes). Default: 5 minutes.
- **SessionTimeoutMinutes:** The duration after which a session is closed (in minutes). Default: 60 minutes.
- **WhiteList:** Comma-separated list of usernames that should not be closed.

Example `app.config` settings:

```xml
<?xml version="1.0" encoding="utf-8" ?>
<configuration>
	<appSettings>
		<add key="TimerIntervalMinutes" value="5" />
		<add key="SessionTimeoutMinutes" value="60" />
		<add key="WhiteList" value="Administrator" />
	</appSettings>
	<startup>
		<supportedRuntime version="v4.0" sku=".NETFramework,Version=v4.7.2" />
	</startup>
</configuration>
```

## Usage

1. **Install the Service:** Use the `--install` argument from the command line.
2. **Start the Service:** Use the `--start` argument.
3. **Stop the Service:** Use the `--stop` argument.
4. **Uninstall the Service:** Use the `--uninstall` argument.

## Contributing

Contributions, bug reports, and feature requests are welcome. Please use the [Issues](https://github.com/kemalincekara/WindowsSessionManager/issues) section for any suggestions or improvements.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.

---

*This project was developed to automate session management. Your feedback is highly appreciated!*