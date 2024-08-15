import React, { useState } from 'react';
import {
  Grid,
  FormLabel,
  OutlinedInput,
  Button,
  Select,
  MenuItem,
  Dialog,
  DialogTitle,
  DialogContent,
  DialogActions   
} from '@mui/material';

import {usePermission} from '../Context/Permission.context'

const PermissionForm = () => {
  
  // contexto 
  const permissionContext = usePermission()
  // data del form
  const [firstName, setFirstName] = useState('');
  const [lastName, setLastName] = useState('');
  const [permissionType, setPermissionType] = useState(0);
  // new permission description
  const [newPermission, setNewPermission] = useState('');
  // modal
  const [open, setOpen] = useState(false);
  
  // modal metodos
  const handleClickOpen = () => {
    setOpen(true);
  };
  const handleClose = () => {
    setOpen(false);
  };

  // cambia el valor de las variables de estado 
  const handleInputChange = (event) => {
    const { name, value } = event.target; // Desestructuramos el nombre y el valor del evento
    switch (name) {
      case 'nameEmployee':
        setFirstName(value);
        break;
      case 'lastNameEmployee':
        setLastName(value);
        break;
      case 'permissionType':
        setPermissionType(value);
        break;
      case 'newPermission':
        setNewPermission(value);
        break;
      default:
        break;
    }
    
  };
  
  const handlePermissionTypeChange = (event) => {
    console.log(event.target.value)
    setPermissionType(event.target.value);
  };

  const handleSubmit = async (event) => {
    event.preventDefault();
    // Aquí puedes agregar la lógica para enviar el formulario
    await permissionContext.createPermission(firstName, lastName, permissionType)
  };
     
     
     
     
  return (
    <Grid container>
      <form onSubmit={handleSubmit}>
        <Grid container spacing={3}>
        <Grid item xs={12} >
        <Grid container spacing={3} justifyContent={'flex-end'}>
            <Button variant="contained" xs={{marginLeft: 'auto'}} onClick={handleClickOpen}>
              Nuevo Tipo
            </Button>
        </Grid>
          </Grid>
          <Grid item xs={12} >
            <FormLabel   htmlFor="nameEmployee" required>
              Nombre
            </FormLabel>
            <OutlinedInput
              id="nameEmployee"
              name="nameEmployee"
              type="text"
              value={firstName}
              onChange={handleInputChange}
              placeholder="John"
              autoComplete="given-name"
              required
              fullWidth
            />
          </Grid>
          <Grid item xs={12} >
            <FormLabel htmlFor="lastNameEmployee" required>
              Apellido
            </FormLabel>
            <OutlinedInput
              id="lastNameEmployee"
              name="lastNameEmployee"
              value={lastName}
              onChange={handleInputChange}
              type="text"
              placeholder="Doe"
              autoComplete="family-name"
              required
              fullWidth
            />
          </Grid>
          <Grid item xs={12}>
            <FormLabel htmlFor="permissionType" required>
              Tipo de Permiso
            </FormLabel>
            <Select
              id="permissionType"
              name="permissionType"
              value={permissionType}
              onChange={handleInputChange}
              displayEmpty
              required
              fullWidth
            >
              <MenuItem value={0}>
                <em>Seleccione un tipo de permiso</em>
              </MenuItem>
              {
                permissionContext?.permissionsType.map((pt,index) => (
                  <MenuItem key={index} value={pt.id}>
                    {pt.description}
                  </MenuItem>
                ))
              }
            </Select>
          </Grid>
        
          <Grid item xs={12} >
            <Button type="submit" variant="contained" size='large' color="primary">
              Enviar
            </Button>
          </Grid>
        </Grid>
      </form>

    {/* Modal para crear nuevo tipo de permiso */}
    <Dialog open={open} onClose={handleClose}>
      <DialogTitle>Nuevo Tipo de Permiso</DialogTitle>
      <DialogContent>
        <Grid>
            <FormLabel htmlFor="newPermission" required>
              Descripcion del permiso
            </FormLabel>
            <OutlinedInput
              id="newPermission"
              name="newPermission"
              type="text" 
              placeholder="Permission"
              required
              fullWidth
            />
        </Grid>
      </DialogContent>
      <DialogActions>
        <Button onClick={handleClose} color="primary">
          Cancelar
        </Button>
        <Button onClick={handleClose} color="primary">
          Guardar
        </Button>
      </DialogActions>
    </Dialog>

    </Grid>
  );
}

export default PermissionForm;