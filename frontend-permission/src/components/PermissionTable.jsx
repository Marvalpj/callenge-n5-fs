import React from 'react';

import { Edit } from '@mui/icons-material';

import Button from '@mui/material/Button';
import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableContainer from '@mui/material/TableContainer';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';
import Paper from '@mui/material/Paper';

import { usePermission } from '../Context/Permission.context'

const PermissionTable = () => {

  //const permissionContext = usePermission();
  const {
    permissions,
    setPermissionFormData,
    setStatusApp,
  } = usePermission();
  
  const [dataTable, setDataTable] = React.useState([])

  React.useEffect(() => {

    if (permissions) {
      const newData = permissions.map(item =>
        createData(item.id, item.nameEmployee, item.lastNameEmployee, item.permissionTypeId, item.permissionType, item.date)
      );
      // Actualiza el estado con el nuevo array
      setDataTable(newData);
    }
  }, [permissions]);

  const createData = (id, nameEmployee, lastNameEmployee, permissionTypeId, permissionType, date) => {
    const dateTemp = (new Date(date)).toLocaleDateString()
    return { id, nameEmployee, lastNameEmployee, permissionTypeId, permissionType, date: dateTemp};
  }

  const editHandler = (permission) => {
    
    console.log()

    setPermissionFormData({
      id: permission.id,
      nameEmployee: permission.nameEmployee,
      lastNameEmployee: permission.lastNameEmployee,
      permissionTypeId: permission.permissionTypeId,
    })
    setStatusApp('UPDATE')
  }

  return (
    <TableContainer component={Paper}>
      <Table sx={{ minWidth: 650 }} md={{ maxHeight: 550 }} aria-label="simple table">
        <TableHead>
          <TableRow>
            <TableCell>Id</TableCell>
            <TableCell align="right">Nombre</TableCell>
            <TableCell align="right">Apellido</TableCell>
            <TableCell align="right">Tipo Permiso</TableCell>
            <TableCell align="right">Fecha</TableCell>
            <TableCell align="right">Editar</TableCell>
          </TableRow>
        </TableHead>
        <TableBody>
          {dataTable?.map((item, index) => (
            <TableRow
              key={index}
              sx={{ '&:last-child td, &:last-child th': { border: 0 } }}
            >
              <TableCell component="th" scope="row">
                {item.id}
              </TableCell>
              <TableCell align='right'>{item.nameEmployee}</TableCell>
              <TableCell align="right">{item.lastNameEmployee}</TableCell>
              <TableCell align="right">{item.permissionType}</TableCell>
              <TableCell align="right">{item.date}</TableCell>
              <TableCell align="right">
              <Button onClick={() => editHandler(item)} variant={'contained'} >
                <Edit /> 
              </Button>
              </TableCell>
            </TableRow>
          ))}
        </TableBody>
      </Table>
    </TableContainer>
  );
}

export default PermissionTable;