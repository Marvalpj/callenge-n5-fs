import { useState } from 'react'
import './App.css'
import API from './api'
import Box from '@mui/material/Box';
import Button from '@mui/material/Button';
import Grid from '@mui/material/Grid';
import TextField from '@mui/material/TextField';
import Select from '@mui/material/Select';
import MenuItem from '@mui/material/MenuItem';
import FormControl from '@mui/material/FormControl';
import InputLabel from '@mui/material/InputLabel';




function App() {
  const [count, setCount] = useState(0)
  const [permission, setPermission] = useState('');

  const handleChange = (event) => {
    setPermission(event.target.value);
  };

  const handleTestingAxios = (e) => {
    e.preventDefault();

    API.get(`/pokemon/ditto`)
      .then(res => {
        console.log(res.data);
      })
  }

  const handleSubmit = (e) => {
    console.log("handleSubmit")
    console.log("permission ", permission)
    const formData = new FormData(e.currentTarget);
    e.preventDefault();
    for (let [key, value] of formData.entries()) {
      console.log(key, value);
    }
  }



  return (
    <>

      <div>
        <h1>Permission App</h1>
        <form onSubmit={handleSubmit}>
        <Grid container spacing={3} maxWidth={600}>
          <Grid item xs={6}>
              <TextField fullWidth id="outlined-basic" label="Nombre Empleado" variant="outlined" name='name' />     
          </Grid>
          <Grid item xs={6}>
              <TextField fullWidth id="outlined-basic" label="Apellido Empleado" variant="outlined" name='lastname' />
          </Grid>
          <Grid item xs={6}>
              <FormControl fullWidth>
                <InputLabel id="demo-simple-select-label">Tipo de Permiso</InputLabel>
                <Select
                  labelId="demo-simple-select-label"
                  id="demo-simple-select"
                  value={permission}
                  label="Tipo de Permiso"
                  onChange={handleChange}
                >
                  <MenuItem value={"Owner"}>Owner</MenuItem>
                  <MenuItem value={"Admin"}>Admin</MenuItem>
                  <MenuItem value={"Member"}>Member</MenuItem>
                </Select>
              </FormControl>
 
          </Grid>
          <Grid item xs={6}>
              <TextField fullWidth id="outlined-basic" variant="outlined" name='datePermission' type='date' />
          </Grid>
          <Grid item xs={12}>
      
              <Button fullWidth variant="contained" size='large' type='submit'>
                submit
              </Button>
      
          </Grid>

        </Grid>
        </form>
       
      </div>

     
      
      <Box component="section" sx={{ p: 2, border: '1px dashed grey', display: 'flex', justifyContent: "space-between", marginTop: '100px' }}>
      <Button fullWidth variant="contained" size='large' onClick={handleTestingAxios}>
          Testing axios        
      </Button>
      </Box>

    </>
  )
}

export default App
