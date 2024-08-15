import { useState } from 'react'
import './App.css'
// components @mui
import API from './api'
import Grid from '@mui/material/Grid';
import Card from '@mui/material/Card';
import CardHeader from '@mui/material/CardHeader';
import CardContent from '@mui/material/CardContent';
// sonnner 
import { Toaster } from 'sonner';
// components propios
import PermissionForm from './components/PermissionForm'
import PermissionTable from './components/PermissionTable'

// context 
import { PermissionProvider} from './Context/Permission.context'

function App() {

  return (
    <PermissionProvider>
      {/*notificaciones */}
      <Toaster position="top-right" />

      <Grid container spacing={3} sx={{ padding: 2 }}>
        <Grid item xs={12}>
          <Card sx={{ width: '100%' }}>
            <CardHeader title="Gestion de permisos"
              sx={{
                textAlign: 'left'
              }} />
            <CardContent>
              
            </CardContent>
          </Card>
        </Grid>
      </Grid>

      {/* <Grid container spacing={3} sx={{ padding: 2 }}>
        <Grid item xs={12} >
          <Card sx={{ width: '100%' }}>
            <CardHeader title="Permisos"
              sx={{
                textAlign: 'left'
              }} />
            <CardContent>
              <PermissionTable />
            </CardContent>
          </Card>
        </Grid>
      </Grid> */}

      <Grid container spacing={3} sx={{ padding: 2 }}>
        <Grid item xs={12} md={5}>
          <Card sx={{ width: '100%', height: 500 }}>
            <CardHeader title="Crear de permisos"
              sx={{
                textAlign: 'left'
              }} />
            <CardContent>
              <PermissionForm />
            </CardContent>
          </Card>
        </Grid>
        <Grid item xs={12} md={7}>
          <Card sx={{ width: '100%', height: '100%', maxHeight: 500, overflowY: 'auto' }}>
            <CardHeader title="Permisos"
              sx={{
                textAlign: 'left'
              }} />
            <CardContent>
              <PermissionTable />
            </CardContent>
          </Card>
        </Grid>
      </Grid>
    </PermissionProvider>
  )
}

export default App
