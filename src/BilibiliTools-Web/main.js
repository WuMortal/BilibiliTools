const { app, BrowserWindow } = require('electron')
const url = require("url");
const path = require("path");

let mainWindow

function createWindow() {
    mainWindow = new BrowserWindow({
        width: 1054,
        height: 800,
        webPreferences: {
            nodeIntegration: true
        }
    })

    mainWindow.loadURL(`/dist/BilibiliTools-Web/index.html`)
    
    // Open the DevTools.
    mainWindow.webContents.openDevTools()

    mainWindow.on('closed', function () {
        mainWindow = null
    })
}

app.on('ready', createWindow)

app.on('window-all-closed', function () {
    if (process.platform !== 'darwin') app.quit()
})

app.on('activate', function () {
    if (mainWindow === null) createWindow()
})
